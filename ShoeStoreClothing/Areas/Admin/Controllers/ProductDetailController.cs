using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductDetailController : Controller
	{
		private readonly IProductDetailRepository _productDetailRepository;
		private readonly ApplicationDbContext _context;
		private readonly IPhotoService _photoService;	
		public ProductDetailController(IProductDetailRepository productDetailRepository,IPhotoService photoService, ApplicationDbContext context)
		{
			_productDetailRepository = productDetailRepository;
			_context = context;
			_photoService = photoService;
		}
		public IActionResult Index(string? search,int page=1)
		{
			var listProductDetail = new ListProductDetailViewModel()
			{
				productDetails = _productDetailRepository.GetAllProductDetail(search, page),
				TotalPage = (int)Math.Ceiling(_productDetailRepository.GetCount(search) / (double)MyHelper.PAGE),
				page=page,
				search=search
			};
			return View(listProductDetail);
		}
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.Colors=_context.Colors.ToList();
			ViewBag.Sizes=_context.Sizes.ToList();
			ViewBag.Products=_context.Products.ToList();
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(ProductDetailViewModel model,string[] SizeIDs, string[] ColorIDs) {
			if (ModelState.IsValid) {
				var result = await _photoService.AddPhotoAysnc(model.Image);
				for(int i = 0; i < ColorIDs.Length; i++)
				{
					for(int j = 0; j < SizeIDs.Length; j++)
					{
                        var productDetail = new ProductDetail()
                        {
                            ProductID = model.ProductID,
                            ColorID = int.Parse(ColorIDs[i]),
                            SizeID = int.Parse(SizeIDs[j]),
                            Image=result.Url.ToString(),
                        };
                        _productDetailRepository.Add(productDetail);
                    }
                }	
                TempData["SuccessMessage"] = "ProductDetail added successfully!";
				return RedirectToAction("Index");
			}
			else
			{
                ModelState.AddModelError("", "Photo upload failed");
            }
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var produtDetail = await _productDetailRepository.GetProductDetailById(id);
			var ColorID = produtDetail.ColorID;
			var colorSort = _context.Colors.OrderBy(c => c.ColorID != ColorID).ThenBy(c => c.ColorID).ToList();
			ViewBag.Colors = new SelectList(_context.Colors.ToList(), "ColorID", "ColorName", produtDetail.ColorID);
			ViewBag.Sizes = new SelectList(_context.Sizes.ToList(), "SizeID", "SizeName", produtDetail.SizeID);
			ViewBag.ProductName=produtDetail.Product?.ProductName;
			ViewBag.urlImage=produtDetail.Image.ToString();
			var model = new EditProductDetailViewModel()
			{
				ProductDetailID = produtDetail.ProductDetailID,
				ColorID = produtDetail.ColorID,
				SizeID = produtDetail.SizeID,
				ProductPrice = produtDetail.ProductPrice,
				ProductSale=produtDetail.PriceSale,
				StockQuantity=produtDetail.StockQuantity,	
			};
            return View(model);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id,EditProductDetailViewModel model) {
            var productDetail = await _productDetailRepository.GetProductDetailById(id);
            ViewBag.Colors = new SelectList(_context.Colors.ToList(), "ColorID", "ColorName", productDetail.ColorID);
            ViewBag.Sizes = new SelectList(_context.Sizes.ToList(), "SizeID", "SizeName", productDetail.SizeID);
            ViewBag.ProductName = productDetail.Product?.ProductName;
            ViewBag.urlImage = productDetail.Image.ToString();
            if (ModelState.IsValid)
			{
				try
				{
					_context.Database.BeginTransaction();
                    productDetail.PriceSale = model.ProductSale;
                    productDetail.ProductPrice = model.ProductPrice;
                    productDetail.StockQuantity = model.StockQuantity;
					productDetail.ColorID= model.ColorID;
					productDetail.SizeID = model.SizeID;
                    _context.SaveChanges();
					_context.Database.CommitTransaction();
                    TempData["SuccessMessage"] = "ProductDetail updated successfully!";
                    return RedirectToAction("Index");
                }
                catch (Exception ex) {
					_context.Database.RollbackTransaction();
                    return View(model);
                }
            }
			return View(model);	
        }
	}
}
