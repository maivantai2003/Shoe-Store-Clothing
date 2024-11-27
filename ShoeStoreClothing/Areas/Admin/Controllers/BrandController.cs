using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class BrandController : Controller
	{
		private readonly IBrandRepository _brandRepository;
		public BrandController(IBrandRepository brandRepository)
		{
			_brandRepository = brandRepository;
		}
		public IActionResult Index(string? search,int page=1)
		{
			var listBrand = new ListBrandViewModel()
			{
				brands = _brandRepository.GetAllBrand(search, page),
				TotalPage = (int)Math.Ceiling(_brandRepository.GetCount(search)/(double)MyHelper.PAGE),
				search=search,
				page=page
			};
			return View(listBrand);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BrandViewModel model) {
			if (ModelState.IsValid)
			{
				var brand = new Brand()
				{
					BrandName = model.BrandName,
					Status = 1
				};
				_brandRepository.Add(brand);
				return RedirectToAction("Index");
            }
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id) { 
			var result=await _brandRepository.GetBrandById(id);
			var brand = new UpdateBrandViewModel()
			{
				BrandName = result.BrandName,
				BrandID = result.BrandID,
			};
			return View(brand);
		}
		[HttpPost]
		public IActionResult Update(int id,UpdateBrandViewModel model)
		{
			if (ModelState.IsValid) {
				var brand = new Brand()
				{
					BrandID = model.BrandID,	
					BrandName = model.BrandName,
					Status = 1
				};
				_brandRepository.Update(brand);
				return RedirectToAction("Index");
			}
			return View(model);
		}

	}
}
