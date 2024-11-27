using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;
using System.Drawing;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class MaterialController : Controller
	{
		private readonly IMaterialRepository _materialRepository;
		public MaterialController(IMaterialRepository materialRepository)
		{
			_materialRepository = materialRepository;
		}
		public IActionResult Index(string? search,int page=1)
		{
			var listMaterial = new ListMaterialViewModel()
			{
				materials = _materialRepository.GetAllMaterial(search, page),
				page = page,
				search = search,
				TotalPage = (int)Math.Ceiling(_materialRepository.GetCount(search) / (double)MyHelper.PAGE)
			};
			return View(listMaterial);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(MaterialViewModel model)
		{
			if (ModelState.IsValid) {
				var material = new Material()
				{
					MaterialName = model.MaterialName,
					Status = 1
				};
				_materialRepository.Update(material);
                TempData["SuccessMessage"] = "Material added successfully!";
				return RedirectToAction("Index");
            }
			return View(model);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var model=await _materialRepository.GetMaterialById(id);
			var editmaterial = new EditMaterialViewModel()
			{
				MaterialName = model.MaterialName,
				MaterialID = model.MaterialID,
			};
			return View(editmaterial);
		}
		[HttpPost]
		public IActionResult Update(EditMaterialViewModel model)
		{
			if (ModelState.IsValid)
			{
				var material = new Material()
				{
					MaterialID = model.MaterialID,
					MaterialName = model.MaterialName,
					Status = 1
				};
				_materialRepository.Update(material);
                TempData["SuccessMessage"] = "Material updated successfully!";
				return RedirectToAction("Index");
            }
			return View(model);
		}

	}
}
