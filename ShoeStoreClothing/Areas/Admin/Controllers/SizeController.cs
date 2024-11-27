using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SizeController : Controller
	{
		private readonly ISizeRepository _sizeRepository;
		public SizeController(ISizeRepository sizeRepository)
		{
			_sizeRepository = sizeRepository;
		}

		public IActionResult Index(string? search, int page=1)
		{
			var listsize = new ListSizeViewModel()
			{
				Sizes = _sizeRepository.GetAllSize(search, page),
				page = page,
				TotalPage = (int)Math.Ceiling(_sizeRepository.GetCount(search) / (double)MyHelper.PAGE),
				search=search
			};
			return View(listsize);
		}
		[HttpGet]
		public IActionResult Create() { 
			return View();
		}
		[HttpPost]
		public IActionResult Create(SizeViewModel model) { 
			if(ModelState.IsValid)
			{
				var size = new Size()
				{
					SizeName = model.SizeName,
					Status = 1
				};
				_sizeRepository.Add(size);
				TempData["SuccessMessage"] = "Color added successfully!";
				return RedirectToAction("Index");	
			}
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id) {
			var result=await _sizeRepository.GetSizeById(id);
			var size = new Size()
			{
				SizeID = result.SizeID,
				SizeName = result.SizeName,
			};
			return View(size);
		}
		[HttpPost]
		public IActionResult Update(EditSizeViewModel model) {
			if (ModelState.IsValid)
			{
				var size = new Size()
				{
					SizeID = model.SizeID,
					SizeName = model.SizeName,
					Status = 1
				};
				_sizeRepository.Update(size);
				return RedirectToAction("Index");	
			}
			return View(model);
		}
	}
}
