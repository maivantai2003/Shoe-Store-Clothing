using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Hubs;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class DiscountController(ApplicationDbContext _context,IHubContext<myHub> _hubContext) : Controller
	{
		public IActionResult Index(string ?search,int page=1)
		{
			var query = _context.Discounts.AsQueryable();
			var Count = 0;
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.DiscountName.Contains(search));
			}
			Count = query.Count();
			var result = PageList<Discount>.Create(query, MyHelper.PAGE, page);
			var listDiscount = new ListDiscountViewModel()
			{
				Discounts = result.Select(x => x),
				TotalPage = (int)Math.Ceiling(Count / (double)MyHelper.PAGE),
				search = search,
				page = page
			};
			return View(listDiscount);
		}
		public IActionResult Add()
		{
			ViewBag.listProduct=_context.ProductDetails.Include(x=>x.Product).Include(x=>x.Color).Include(x=>x.Size);
			return View();
		}
	}
}
