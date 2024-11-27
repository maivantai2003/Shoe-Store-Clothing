using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Route("Admin/[controller]/[action]")]
	public class CategoryController : Controller
	{
		private readonly ICategoryRepository _categoryRepository;
		public CategoryController(ICategoryRepository categoryRepository)
		{
			_categoryRepository = categoryRepository;
		}

		public IActionResult Index(string? search,int page=1)
		{
			var listCategories = new ListCategoryViewModel()
			{
				categories = _categoryRepository.GetAllCategories(search, page),
				TotalPage = (int)Math.Ceiling(_categoryRepository.GetCount(search) / (double)MyHelper.PAGE),
				search=search,
				page=page
			};
			return View(listCategories);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(CategoryViewModel model)
		{
			if (ModelState.IsValid) {
				var catagory = new Category()
				{
					CategoryName = model.CategoryName,
					Status = 1
				};
				_categoryRepository.Add(catagory);
                TempData["SuccessMessage"] = "Category added successfully!";
                return RedirectToAction("Index");
			}
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id) { 
			var result=await _categoryRepository.GetCategoryById(id);
			var model = new EditCategoryViewModel()
			{
				CategoryID = id,
				CategoryName = result.CategoryName,
			};
			return View(model);
		}
		[HttpPost]
		public IActionResult Update(EditCategoryViewModel model) {
			if (ModelState.IsValid) {
				var category = new Category()
				{
					CategoryName = model.CategoryName,
					CategoryID = model.CategoryID,
					Status = 1
				};
                TempData["SuccessMessage"] = "Category added successfully!";
				_categoryRepository.Update(category);
				return RedirectToAction("Index");
            }
			return View(model);
		}
	}
}
