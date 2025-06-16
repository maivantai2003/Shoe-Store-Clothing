using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using QuickMailer;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Hubs;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;
using System.Net;
using MailKit.Net.Smtp;
using System.Text;
using System.Text.Json.Nodes;
using CloudinaryDotNet;
using Azure.Core;
using Microsoft.AspNetCore.Server.Kestrel.Core.Internal.Http;
namespace ShoeStoreClothing.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<myHub> _hubContext;
        private readonly IConfiguration _configuration;
        private readonly PaypalAccount paypalAccount;
        public CartController(ApplicationDbContext context,UserManager<AppUser> userManager, IHubContext<myHub> hubContext,IConfiguration configuration)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
            _configuration = configuration;
            paypalAccount=new PaypalAccount() {
                PaypalClientId = _configuration["PayPalSettings:ClientId"],
                PaypalSecret = _configuration["PayPalSettings:Secret"],
                PaypalUrl = _configuration["PayPalSettings:Url"]
            };
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddToCard(string id,string quantity,string price)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return View("/404");
            }
            var result = _context.ShoppingCarts.FirstOrDefault(x => x.CustomerID == user.Id && x.ProductDetailID == Convert.ToInt32(id));
            try
            {
                _context.Database.BeginTransaction();
                if (result == null)
                {
                    var shoppingCart = new ShoppingCart()
                    {
                        ProductDetailID = Convert.ToInt32(id),
                        CustomerID = user.Id,
                        Quantity = Convert.ToInt32(quantity),
                        Price=Convert.ToDouble(price)
                    };
                    Json(shoppingCart);
                    _context.ShoppingCarts.Add(shoppingCart);
                }
                else
                {
                    result.Price = Convert.ToDouble(price);
                    result.Quantity += Convert.ToInt32(quantity);
                }
                _context.SaveChanges();
                _context.Database.CommitTransaction();
            }
            catch (Exception ex) { 
                _context.Database.RollbackTransaction();
            }
            return RedirectToAction("Detail","Home",new {id});
        }
        public IActionResult DeleteCart(string id)
        {
            var cart = _context.ShoppingCarts.FirstOrDefault(x => x.ShoopingCartID == Convert.ToInt32(id));
            if (cart == null)
            {
                return View("/404");
            }
            _context.Remove(cart); 
            _context.SaveChanges();
            return RedirectToAction("Checkout","Cart");
        }
        public async Task<IActionResult> Checkout()
        {
            var customer =await _userManager.GetUserAsync(User);
            if (customer == null)
            {
                return View("/404");
            }
            var customerID=customer.Id;
            ViewBag.Name = customer.FullName;
            ViewBag.Phone = customer.PhoneNumber;
            ViewBag.Email = customer.Email;
            ViewBag.CustomerID=customerID;  
            ViewBag.PaypalClientId=paypalAccount.PaypalClientId;
            var list = _context.ShoppingCarts.Where(x => x.CustomerID == customerID).Include(x=>x.ProductDetail).Include(x=>x.ProductDetail.Size).Include(x=>x.ProductDetail.Product);
            var shoppingCart = new ListShoppingCartViewModel()
            {
                shoppingCarts = list.ToList(),
                TotalPrice = list.Sum(x => x.Price * x.Quantity),
                Discount = list.Sum(x => x.ProductDetail.PriceSale * x.Quantity)
            };
            return View(shoppingCart);
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(string[] cart, string[] quantity,string customerID,string Email,string FullName)
        {
            return Json(new { cart, quantity, customerID, Email, FullName });
            var success = await ProcessOrderAsync(cart, quantity, customerID, Email, FullName);
            if (success) { 
                return RedirectToAction("Shop", "Home");
            }
            else {
                return View("/500");
            }
        }
        private async Task<bool> ProcessOrderAsync(string[] cart, string[] quantity, string customerID, string Email, string FullName)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var Invoice = new Invoice()
                {
                    CustomerID = customerID,
                };
                await _context.AddAsync(Invoice);
                await _context.SaveChangesAsync();
                double TotalAmount = 0d;
                double TotalDiscount = 0d;
                for (int i = 0; i < cart.Length; i++)
                {
                    int id = int.Parse(cart[i]);
                    var cartItem = await _context.ShoppingCarts.Include(x => x.ProductDetail).FirstOrDefaultAsync(x => x.ShoopingCartID == id);
                    var productDetail = await _context.ProductDetails.FirstOrDefaultAsync(x => x.ProductDetailID == cartItem.ProductDetailID);
                    //return Json(cartItem);
                    double Discount = cartItem.ProductDetail.PriceSale * Convert.ToInt32(quantity[i]);
                    TotalDiscount += Discount;
                    double ProductPrice = cartItem.Price;
                    TotalAmount += int.Parse(quantity[i]) * ProductPrice;
                    var detailInvoice = new InvoiceDetail()
                    {
                        InvoiceID = Invoice.InvoiceID,
                        ProductDetailID = cartItem.ProductDetailID,
                        Quantity = int.Parse(quantity[i]),
                        ProductPrice = ProductPrice,
                        Discount = Discount,
                        TotalPrice = ProductPrice - Discount,
                    };
                    await _context.AddAsync(detailInvoice);
                    await _context.SaveChangesAsync();
                    productDetail.StockQuantity -= int.Parse(quantity[i]);
                    await _context.SaveChangesAsync();
                    _context.Remove(cartItem);
                    await _context.SaveChangesAsync();
                }
                Invoice.TotalDiscount = TotalDiscount;
                Invoice.TotalAmount = TotalAmount;
                Invoice.FinalAmount = TotalAmount - TotalDiscount;
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
                await _hubContext.Clients.Group("Admin").SendAsync("LoadInvoice");
                return true;
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
                return false;
            }
        }
        [HttpPost]
        public async Task<IActionResult> PayWithPaypal(string[] cart, string[] quantity, string customerID, string Email, string FullName,string totalDiscount,string totalAmount)
        {
            ViewBag.PaypalClientId = paypalAccount.PaypalClientId;
            decimal total=decimal.Parse(totalAmount)-decimal.Parse(totalDiscount);

            decimal usdAmount = (total / formatMoney.USD);
            ViewBag.UsdAmount = usdAmount.ToString("F2");
            //return Json(new { cart, quantity, customerID, Email, FullName,totalDiscount,totalAmount,total, price=usdAmount.ToString("F2")});
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> CheckoutPaypal([FromBody] JsonObject? data)
        {
            var totalAmount = data?["amount"]?.ToString();
            if (totalAmount == null) {
                return new JsonResult(new { Id = "" });
            }

            JsonObject createOrderRequest = new JsonObject();
            createOrderRequest.Add("intent", "CAPTURE");

            JsonObject amount = new JsonObject();
            amount.Add("currency_code", "USD");
            amount.Add("value", totalAmount);

            JsonObject purchaseUnit1 = new JsonObject();
            purchaseUnit1.Add("amount", amount);
            
            JsonArray purchaseUnits = new JsonArray();
            purchaseUnits.Add(purchaseUnit1);

            createOrderRequest.Add("purchase_units", purchaseUnits);
            string accessToken=await GetPaypalAccessToken();
            string url = paypalAccount.PaypalUrl + "/v2/checkout/orders";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);
                requestMessage.Content = new StringContent(createOrderRequest.ToString(), null, "application/json");

                var httpResponse = await client.SendAsync(requestMessage);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);

                    if (jsonResponse != null)
                    {
                        string paypalOrderId = jsonResponse["id"]?.ToString() ?? "";
                        return new JsonResult(new { Id = paypalOrderId });
                    }
                }

            }

            return new JsonResult(new { Id = "" });
        }
        [HttpPost]
        public async Task<JsonResult> CompletePaypal([FromBody] JsonObject data)
        {
            var orderId = data?["orderID"]?.ToString();
            if (orderId == null)
            {
                return new JsonResult("error");
            }

            // Lấy access token từ PayPal
            string accessToken = await GetPaypalAccessToken();
            string url=paypalAccount.PaypalUrl+"/v2/checkout/orders/"+orderId+"/capture";
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + accessToken);

                var requestMessage = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);
                requestMessage.Content = new StringContent("", null, "application/json");

                var httpResponse = await client.SendAsync(requestMessage);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();
                    var jsonResponse = JsonNode.Parse(strResponse);

                    if (jsonResponse != null)
                    {
                        string paypalOrderStatus = jsonResponse["status"]?.ToString() ?? "";
                        if (paypalOrderStatus == "COMPLETED")
                        {
                            // TODO: Lưu đơn hàng vào cơ sở dữ liệu ở đây
                            // Ví dụ:
                            // var order = new Order { ... };
                            // _dbContext.Orders.Add(order);
                            // await _dbContext.SaveChangesAsync();

                            return new JsonResult("success");
                        }
                    }
                }
            }

            return new JsonResult("error");
        }

        private async Task<string> GetPaypalAccessToken()
        {
            string accessToken = "";
            string url=paypalAccount.PaypalUrl+"/v1/oauth2/token";
            using (var client = new HttpClient())
            {
                string credentials64 =
                Convert.ToBase64String(Encoding.UTF8.GetBytes(paypalAccount.PaypalClientId + ":" + paypalAccount.PaypalSecret));

                client.DefaultRequestHeaders.Add("Authorization", "Basic " + credentials64);

                var requestMessage = new HttpRequestMessage(System.Net.Http.HttpMethod.Post, url);
                requestMessage.Content = new StringContent("grant_type=client_credentials", null, "application/x-www-form-urlencoded");
                var httpResponse=await client.SendAsync(requestMessage);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var strResponse = await httpResponse.Content.ReadAsStringAsync();

                    var jsonResponse = JsonNode.Parse(strResponse);
                    if (jsonResponse != null)
                    {
                        accessToken = jsonResponse["access_token"]?.ToString() ?? "";
                    }
                }

            }
            return accessToken; 
        }
    }
}
