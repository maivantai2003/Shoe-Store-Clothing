using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class PurchaseOrderController : Controller
	{
		private readonly ApplicationDbContext _context;
		public PurchaseOrderController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Suppliers =new SelectList(_context.Suppliers,"SupplierID","SupplierName");
			ViewBag.ProductDetails =_context.ProductDetails.Include(x=>x.Product).Include(x=>x.Color).Include(x=>x.Size);
			return View();
		}
		[HttpPost]
		public IActionResult Create(string[] productDetailId,int supplier,string[] quantity, string[] price)
		{
            ViewBag.Suppliers = new SelectList(_context.Suppliers, "SupplierID", "SupplierName");
            ViewBag.ProductDetails = _context.ProductDetails.Include(x => x.Product).Include(x => x.Color).Include(x => x.Size);
            return Json(new { productDetailId, quantity, price });
		}
	}
}
