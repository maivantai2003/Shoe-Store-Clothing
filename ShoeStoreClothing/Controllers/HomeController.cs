using CloudinaryDotNet.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;
using System.Diagnostics;
using System.Security.Claims;

namespace ShoeStoreClothing.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public HomeController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var listProductDetail = _context.ProductDetails.Include(p=>p.Product).Include(p=>p.Color).Include(p=>p.Size).Where(p=>p.ProductPrice!=0).ToList();
            return View(listProductDetail);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Shop(string? search,string? brand,string? category,int page=1)
        {
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Brands=_context.Brands.ToList();
            //
            var listProductDetail = _context.ProductDetails.Include(x=>x.Color).Include(x=>x.Size).Include(x=>x.Product).AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                listProductDetail = listProductDetail.Where(x => x.Product.ProductName.Contains(search) || x.ProductDetailID.Equals(search));
            }
            if (!string.IsNullOrEmpty(brand))
            {
                listProductDetail = listProductDetail.Where(x => x.Product.Brand.BrandName.Contains(brand));
            }
            if (!string.IsNullOrEmpty(category))
            {
                listProductDetail = listProductDetail.Where(x => x.Product.Category.CategoryName.Contains(category));
            }
            listProductDetail = listProductDetail.Where(x => x.ProductPrice != 0);
            var totalRecord=listProductDetail.Count();
            var totalPage = listProductDetail.Count();
            var result = PageList<ProductDetail>.Create(listProductDetail,9, page);
            var listShopProductDetail=new ListShopProductDetailViewModel()
            {
                productDetails= result,
                search=search,
                brand=brand, 
                category=category,
                TotalPage=(int)Math.Ceiling(totalPage/(double)9),
                page=page,
                TotalRecord=totalRecord
            };
            return View(listShopProductDetail);  
        }
        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            var result=await _context.ProductDetails.Include(x=>x.Product).Include(x=>x.Color).Include(x=>x.Size).FirstOrDefaultAsync(x=>x.ProductDetailID==id);
            var detailProduct = new DetailProductViewModel()
            {
                productDetailId = id,
                SizeName = result.Size?.SizeName,
                ProductName=result.Product?.ProductName,
                Color=result.Color.ColorName,
                PriceSale=result.PriceSale,
                ProductPrice=result.ProductPrice,   
                Image=result.Image,
                Brand=result.Product.Brand?.BrandName,
                Quantity=result.StockQuantity
            };
            return View(detailProduct);
        }
        [Route("/404")]
        public IActionResult PageNotFound()
        {
            return View();
        }
        [Authorize]
        [Route("/Order")]
        public async Task<IActionResult> Order()
        {
            var customer=await _userManager.GetUserAsync(User);
            if (customer==null)
            {
                return Redirect("/404");
            }
            var Orders = _context.Invoices.Include(x=>x.InvoiceDetails).ThenInclude(x=>x.ProductDetail).
                ThenInclude(x=>x.Product).Include(x=>x.InvoiceDetails).ThenInclude(x=>x.ProductDetail.Size).Include(x => x.InvoiceDetails).
                ThenInclude(x => x.ProductDetail.Color).Where(x=>x.CustomerID==customer.Id);
            return View(Orders);
        }
        public async Task<IActionResult> Chat()
        {
            return View();
        }
        public IActionResult Notification()
        {
            var userId=User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if(userId==null || userId == "")
            {
                return Json(new { });
            }
            var listNotification=_context.Notifications.Where(x=>x.UserId==userId).ToList().OrderBy(x=>x.dateTimeSend);
            return Json(listNotification);  
        }
    }
}
