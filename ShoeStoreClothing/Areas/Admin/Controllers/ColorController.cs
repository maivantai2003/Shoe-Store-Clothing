using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;
using System.Drawing;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ColorController : Controller
	{
		private readonly IColorRepository _colorRepository;
		public ColorController(IColorRepository colorRepository) { 
			_colorRepository = colorRepository;
		}
		public IActionResult Index(string? search,int page=1)
		{
			var listColor = new ListColorViewModel()
			{
				colors = _colorRepository.GetAllColor(search, page),
				search = search,
				TotalPage = (int)Math.Ceiling(_colorRepository.GetCount(search) / (double)MyHelper.PAGE),
				page= page
			};
			return View(listColor);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(ColorViewModel model)
		{
			if (ModelState.IsValid) {
				var color = new ShoeStoreClothing.Models.Color()
				{
					Status = 1,
					ColorName = model.ColorName,
				};
				_colorRepository.Add(color);
                TempData["SuccessMessage"] = "Color added successfully!";
                return RedirectToAction("Index");
			}
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var model=await _colorRepository.GetColorById(id);
			var result = new EditColorViewModel()
			{
				ColorID = model.ColorID,
				ColorName = model.ColorName,
			};
			return View(result);
		}
		[HttpPost]
		public IActionResult Update(EditColorViewModel model)
		{
			if (ModelState.IsValid)
			{
				var color=new ShoeStoreClothing.Models.Color()
				{
					ColorID = model.ColorID,
					ColorName=model.ColorName,	
					Status=1
				};
				_colorRepository.Update(color);
				TempData["SuccessMessage"] = "Color updated successfully!";
				return RedirectToAction("Index");
			}
			return View(model);
		}
	}
}
