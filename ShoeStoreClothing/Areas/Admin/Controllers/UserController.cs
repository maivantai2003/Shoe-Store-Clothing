using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json.Linq;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Hubs;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;
using System.Net.WebSockets;
using System.Security.Claims;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IHubContext<myHub> _hubContext;
		private readonly ApplicationDbContext _context;
		public UserController(UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager, IHubContext<myHub> hubContext, ApplicationDbContext context)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_hubContext = hubContext;
			_context = context;
		}
		public static List<SelectListItem> listItems = new List<SelectListItem>()
		{
		new SelectListItem(){
			Value = "Admin",
				Text = "Admin"
		},new SelectListItem()
		   {
			Value = "Employee",
				Text = "Employee"
			}
	    };
		public IActionResult Index(string? search,int page=1)
		{
			var query = _userManager.Users.AsQueryable();
			var Count = 0;
			if (!string.IsNullOrEmpty(search))
			{
				query= query.Where(x => x.Email.Contains(search));
			}
			Count = query.Count();
			var result = PageList<AppUser>.Create(query, MyHelper.PAGE, page);
			var listUser = new ListUserViewModel()
			{
				Users = result.Select(x=>x),
				TotalPage = (int)Math.Ceiling(Count / (double)MyHelper.PAGE),
				search=search,
				page=page
			};
			return View(listUser);
		}
		[HttpGet]
		public IActionResult Create()
		{
			ViewBag.ListRoles = listItems;
			return View();	
		}
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel userViewModel)
        {
			if (ModelState.IsValid)
			{
				var user=new AppUser()
				{
					FullName=userViewModel.FullName,
					Email=userViewModel.Email,
					UserName=userViewModel.Email
				};
				var result=await _userManager.CreateAsync(user,userViewModel.Password);
				if (result.Succeeded) {
					var role = await _userManager.AddToRoleAsync(user, userViewModel.Role);
					if (role.Succeeded) {
						TempData["SuccessMessage"] = "User added successfully!";
						return RedirectToAction("Index");
					}
				}
				else
				{
					ViewBag.ListRoles = listItems;
					return View(userViewModel);
				}
			}
			ViewBag.ListRoles = listItems;
			return View(userViewModel);
        }
		[AllowAnonymous]
		[HttpPost]
		[HttpGet]
        public async Task<IActionResult> IsEmailExist(string Email)
		{
			var user=await _userManager.FindByEmailAsync(Email);
			if (user == null)
			{
				return Json(true);
			}
			return Json($"Email {Email} is already in use");
		}
		public IActionResult Update() {
			return View();
		}
		public IActionResult Message(string id)
		{
			string userId = "";
			if (User.IsInRole("Admin"))
			{
				userId = "Admin";
			}
			else
			{
				userId= User.FindFirst(ClaimTypes.NameIdentifier).Value;
			}
			var messages = _context.Messages.Where(x => (x.SenderId ==userId && x.ReceiverId == id) || (x.SenderId == id && x.ReceiverId == userId)).ToList();
			return Json(messages);
		}
		public IActionResult ChatUser(string id)
		{
			return View((object)id);
		}
	}
}
