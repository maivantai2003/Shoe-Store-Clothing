using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;
using System.Net.WebSockets;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ProductController : Controller
	{
		private readonly IProductRepository _productRepository;
		private readonly ApplicationDbContext _context;
		public ProductController(IProductRepository productRepository,ApplicationDbContext context)
		{
			_productRepository = productRepository;
			_context = context;	
		}
		public IActionResult Index(string search,int page=1)
		{
			var listProduct = new ListProductViewModel()
			{
				Products = _productRepository.GetAllProduct(search, page),
				TotalPage = (int)Math.Ceiling(_productRepository.GetCount(search) / (double)MyHelper.PAGE),
				search=search,
				page=page
			};
			return View(listProduct);
		}
		[HttpGet]
		public IActionResult Create()
		{
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Materials = _context.Materials.ToList();
            return View();
		}
		[HttpPost]
		public IActionResult Create(ProductViewModel model) {
			if (ModelState.IsValid) {
				var product = new Product()
				{
					ProductName = model.ProductName,
					CategoryID= model.CategoryID,	
					MaterialID= model.MaterialID,
					BrandID= model.BrandID,	
				};
				_productRepository.Add(product);
                TempData["SuccessMessage"] = "Product added successfully!";
                return RedirectToAction("Index");	
			}
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.Materials = _context.Materials.ToList();
            return View(model);	
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var product = await _productRepository.GetProductById(id);
			ViewBag.Categories= new SelectList(_context.Categories.ToList(),"CategoryID","CategoryName",product.CategoryID);
			ViewBag.Materials = new SelectList(_context.Materials.ToList(), "MaterialID", "MaterialName",product.MaterialID);
			ViewBag.Brands = new SelectList(_context.Brands.ToList(), "BrandID", "BrandName", product.BrandID);
			var editProduct = new EditProductViewModel()
			{
				ProductID = product.ProductID,
				ProductName = product.ProductName,
				BrandID = product.BrandID,
				CategoryID= product.CategoryID,
				MaterialID= product.MaterialID,
			};
			return View(editProduct);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id,ProductViewModel model) {
            var product = await _productRepository.GetProductById(id);
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryID", "CategoryName", product.CategoryID);
            ViewBag.Materials = new SelectList(_context.Materials.ToList(), "MaterialID", "MaterialName", product.MaterialID);
            ViewBag.Brands = new SelectList(_context.Brands.ToList(), "BrandID", "BrandName", product.BrandID);
            if (ModelState.IsValid) {
				return Json(model);
			}
			return View(model);
		}

	}
}
