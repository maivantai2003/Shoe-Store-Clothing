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
        public async Task<IActionResult> Checkout(string[] cart, string[] quantity,string customerID,string Email,string FullName, string PaymentMethod)
        {
            return Json(new { cart, quantity, customerID, Email, FullName });
            var success = await ProcessOrderAsync(cart, quantity, customerID, Email, FullName,"COD");
            if (success) { 
                return RedirectToAction("Shop", "Home");
            }
            else {
                return View("/500");
            }
        }
        private async Task<bool> ProcessOrderAsync(string[] cart, string[] quantity, string customerID, string Email, string FullName,string PaymentMethod)
        {
            try
            {
                await _context.Database.BeginTransactionAsync();
                var Invoice = new Invoice()
                {
                    CustomerID = customerID,
                    PaymentMethod = PaymentMethod
                };
                await _context.AddAsync(Invoice);
                await _context.SaveChangesAsync();
                double TotalAmount = 0d;
                double TotalDiscount = 0d;
                var emailContentBuilder = new StringBuilder();
                emailContentBuilder.AppendLine($"<p>Xin chào <b>{FullName}</b>,</p>");
                emailContentBuilder.AppendLine("<p>Đơn hàng của bạn đã được xử lý thành công!</p>");
                emailContentBuilder.AppendLine($"<p><b>Mã hóa đơn:</b> Invoice-{Invoice.InvoiceID}</p>");
                emailContentBuilder.AppendLine("<table border='1' cellspacing='0' cellpadding='5'>");
                emailContentBuilder.AppendLine("<tr><th>Hình ảnh</th><th>Tên sản phẩm</th><th>Số lượng</th></tr>");
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
                    string imageUrl = productDetail?.Image ?? "https://via.placeholder.com/80"; // fallback
                    string productName = productDetail?.Product?.ProductName ?? "Sản phẩm";

                    emailContentBuilder.AppendLine("<tr>");
                    emailContentBuilder.AppendLine($"<td><img src='{imageUrl}' alt='product' width='80'/></td>");
                    emailContentBuilder.AppendLine($"<td>{productName}</td>");
                    emailContentBuilder.AppendLine($"<td>{quantity[i]}</td>");
                    emailContentBuilder.AppendLine("</tr>");
                }
                emailContentBuilder.AppendLine("</table>");
                emailContentBuilder.AppendLine("<p><b>Tổng tiền:</b> " + TotalAmount.ToString("N0") + " VND</p>");
                emailContentBuilder.AppendLine("<p><b>Giảm giá:</b> " + TotalDiscount.ToString("N0") + " VND</p>");
                emailContentBuilder.AppendLine("<p><b>Thành tiền:</b> " + (TotalAmount - TotalDiscount).ToString("N0") + " VND</p>");
                //emailContentBuilder.AppendLine("<p>Cảm ơn bạn đã mua sắm tại cửa hàng của chúng tôi.</p>");
                Invoice.TotalDiscount = TotalDiscount;
                Invoice.TotalAmount = TotalAmount;
                Invoice.FinalAmount = TotalAmount - TotalDiscount;
                await _context.SaveChangesAsync();
                await _context.Database.CommitTransactionAsync();
                await _hubContext.Clients.Group("Admin").SendAsync("LoadInvoice");
                if (Email != null)
                {
                    Task.Run(() => SendGmail.SendAsync(
                        FullName,
                        "vantai08122003@gmail.com",
                        "Đặt hàng thành công tại ShoeStore",
                        emailContentBuilder.ToString()
                    ));
                }
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
            SessionCheckout(cart,quantity,customerID,Email,FullName,totalDiscount,totalAmount,"CREATE");
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
                            var cart = HttpContext.Session.GetString("cart")?.Split(",") ?? Array.Empty<string>();
                            var quantity = HttpContext.Session.GetString("quantity")?.Split(",") ?? Array.Empty<string>();
                            var customerID = HttpContext.Session.GetString("customerID");
                            var Email = HttpContext.Session.GetString("Email");
                            var FullName = HttpContext.Session.GetString("FullName");
                            var totalDiscount = HttpContext.Session.GetString("totalDiscount");
                            var totalAmount = HttpContext.Session.GetString("totalAmount");
                            var result = await ProcessOrderAsync(cart, quantity, customerID, Email, FullName, "PAYPAL");
                            SessionCheckout(cart,quantity,customerID,Email,FullName,"0","0","DELETE");
                            //return Json(new { cart, quantity, customerID, Email, FullName, totalDiscount, totalAmount});
                            if (result)
                            {
                                return new JsonResult("success");
                            }
                            else {
                                return new JsonResult("error");
                            }
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
        private void SessionCheckout(string[] ?cart, string[] ?quantity, string ?customerID, string ?Email, string ?FullName, string ?totalDiscount, string ?totalAmount, string status)
        {
            if (status == "CREATE")
            {
                HttpContext.Session.SetString("cart", string.Join(",", cart));
                HttpContext.Session.SetString("quantity", string.Join(",", quantity));
                HttpContext.Session.SetString("customerID", customerID);
                HttpContext.Session.SetString("Email", Email);
                HttpContext.Session.SetString("FullName", FullName);
                HttpContext.Session.SetString("totalDiscount", totalDiscount);
                HttpContext.Session.SetString("totalAmount", totalAmount);
            }
            else
            {
                HttpContext.Session.Remove("cart");
                HttpContext.Session.Remove("quantity");
                HttpContext.Session.Remove("customerID");
                HttpContext.Session.Remove("Email");
                HttpContext.Session.Remove("FullName");
                HttpContext.Session.Remove("totalDiscount");
                HttpContext.Session.Remove("totalAmount");
            }

        }
    }
}
