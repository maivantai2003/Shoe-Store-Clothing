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
namespace ShoeStoreClothing.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHubContext<myHub> _hubContext;
        public CartController(ApplicationDbContext context,UserManager<AppUser> userManager, IHubContext<myHub> hubContext)
        {
            _context = context;
            _userManager = userManager;
            _hubContext = hubContext;
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
            try
            {
                _context.Database.BeginTransactionAsync();
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
                //try
                //{
                //    //string name,string ToGmail,string Subject,string Body
                //    string Body = "<h1>AAAAAA</h1>";
                //    SendGmail.Send("Văn Tài","vantai08122003@gmail.com","Đơn Hàng Đã Đặt",Body);
                //}
                //catch (Exception ex) {
                //    return Json(ex.Message);  
                //}
                
            }
            catch (Exception ex)
            {
                await _context.Database.RollbackTransactionAsync();
            }
            return RedirectToAction("Shop","Home");
        }
    }
}
