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
	public class InvoiceController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly IHubContext<myHub> _hubContext;

        public InvoiceController(ApplicationDbContext context, IHubContext<myHub> hubContext)
		{
			_context = context;
			_hubContext = hubContext;
		}
		public IActionResult Index(string? search, string? action,int page=1){
			var query=_context.Invoices.Include(x => x.User).AsQueryable();
			if (!string.IsNullOrEmpty(search)) {
				query = query.Where(x=>x.User.FullName.Contains(search));
			}
			/*if (!string.IsNullOrEmpty(action)) {
				int act = action == "Chưa Xử Lý" ? 0 : 1;
				query = query.Where(x => x.Action==act);
			}*/
			int Count = query.Count();
			var result = PageList<Invoice>.Create(query, MyHelper.PAGE, page);
			var listInvoices = new ListInvoiceViewModel()
			{
				invoices = result.Select(x=>x),
				page = page,
				search = search,
				TotalPage = (int)Math.Ceiling(Count / (double)MyHelper.PAGE),
				action=action
			};
			return View(listInvoices);
		}
		[HttpGet]
		public async Task<IActionResult> Detail(int id)
		{
			var Invoice=await _context.Invoices.Include(x=>x.User).Include(x=>x.InvoiceDetails).ThenInclude(x=>x.ProductDetail.Product).FirstOrDefaultAsync(x=>x.InvoiceID==id);
			return View(Invoice);
		}
		[HttpPost]
		public async Task<IActionResult> Update(int id,string action,string customerId)
		{
			try
			{
				_context.Database.BeginTransactionAsync();
                var invoice = await _context.Invoices.FirstOrDefaultAsync(x => x.InvoiceID == id);
                invoice.Action = Convert.ToInt32(action);
                await _context.SaveChangesAsync();
				var notification = new Notifications()
				{
					UserId = customerId,
					notificationContent = "Đơn hàng Order-"+id+" đã được xác nhận và sẽ được chuyển đi sớm nhất"
                };
				await _context.Notifications.AddAsync(notification);	
				await _context.SaveChangesAsync();
				_context.Database.CommitTransactionAsync();	
                await _hubContext.Clients.User(customerId).SendAsync("ProcessOrder", id, "Đã Xác Nhận", notification.dateTimeSend);
                return RedirectToAction("Index");
            }
			catch (Exception ex) { 
				_context.Database.RollbackTransactionAsync();
			}
            return RedirectToAction("Index");
        }
	}
}
