using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class SupplierController : Controller
	{
		private ISupplierRepository _supplierRepository;
		public SupplierController(ISupplierRepository supplierRepository)
		{
			_supplierRepository = supplierRepository;
		}

		public IActionResult Index(string? search,int page=1)
		{
			var listSupplier = new ListSupplierViewModel()
			{
				Suppliers = _supplierRepository.GetAllSupplier(search, page),
				TotalPage = (int)Math.Ceiling((double)_supplierRepository.GetCount(search) / MyHelper.PAGE),
				page=page,
				search=search
			};
			return View(listSupplier);
		}
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(SupplierViewModel supplier) {
			if (ModelState.IsValid) {
				var model = new Supplier()
				{
					SupplierName = supplier.SupplierName,
					Address = supplier.Address,
					PhoneNumber = supplier.PhoneNumber,
					Status=1
				};
				_supplierRepository.Add(model);
				TempData["SuccessMessage"] = "Supplier added successfully!";
				return RedirectToAction("Index");
			}
			return View(supplier);
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var model=await _supplierRepository.GetSupplierById(id);
			var supplier = new EditSupplierViewModel()
			{
				SupplierID = id,
				SupplierName = model.SupplierName,
				Address = model.Address,
				PhoneNumber = model.PhoneNumber,
			};
			return View(supplier);
		}
		[HttpPost]
		public IActionResult Update(EditSupplierViewModel supplier) {

            if (ModelState.IsValid)
			{
				var model = new Supplier()
				{
					SupplierID = supplier.SupplierID,
					SupplierName = supplier.SupplierName,
					Address = supplier.Address,
					PhoneNumber = supplier.PhoneNumber,
					Status = 1
				};
				_supplierRepository.Update(model);
				TempData["SuccessMessage"] = "Supplier updated successfully!";
				return RedirectToAction("Index");

			}
			return View(supplier);
		}
	}
}
