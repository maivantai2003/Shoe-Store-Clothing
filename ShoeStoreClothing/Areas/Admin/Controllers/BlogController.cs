using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.Repository;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController(IBlogRepository _blogRepository) : Controller
    {
        public IActionResult Index(string? search,int page=1)
        {
			var listBlog = new ListBlogViewModel()
			{
				blogs= _blogRepository.GetAllBlog(search,page),
				search = search,
				TotalPage = (int)Math.Ceiling(_blogRepository.GetCount(search) / (double)MyHelper.PAGE),
				page = page
			};
			return View(listBlog);
        }
		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Create(BlogViewModel model) {
			if (ModelState.IsValid)
			{
				Blog blog = new Blog()
				{
					Title = model.Title,
					Subject = model.Subject,
					BlogName = model.BlogName,
				};
				_blogRepository.Add(blog);
				return RedirectToAction("Index");
			}
			return View(model);	
		}
		[HttpGet]
		public async Task<IActionResult> Update(int id)
		{
			var blog = await _blogRepository.GetBlogById(id);
			return View(blog);
		}
		[HttpPost]
		public IActionResult Update(EditBlogViewModel model)
		{
			if (ModelState.IsValid) {
				Blog blog = new Blog()
				{
					BlogId=model.BlogId,
					BlogName= model.BlogName,
					Subject = model.Subject,	
					Title = model.Title,
				};
				_blogRepository.Update(blog);
				return RedirectToAction("Index");
			}
			return View(model);
		}
    }
}
