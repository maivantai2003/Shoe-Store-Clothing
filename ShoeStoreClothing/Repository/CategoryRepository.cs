using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.Repository;

namespace ShoeStoreClothing.Repository
{
	public class CategoryRepository : ICategoryRepository
	{
		private readonly ApplicationDbContext _context;

		public CategoryRepository(ApplicationDbContext context)
		{
			_context = context;
		}

		public bool Add(Category category)
		{
			_context.Add(category);
			return Save();
		}

		public bool Delete(Category category)
		{
			_context.Remove(category);
			return Save();
		}

		public IEnumerable<Category> GetAllCategories(string search,int page=1)
		{
			var listcategories = _context.Categories.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				listcategories = listcategories.Where(x => x.CategoryName.Contains(search));
			}
			var result = PageList<Category>.Create(listcategories, MyHelper.PAGE, page);
			return result.Select(x => x).ToList();
		}
		public int GetCount(string? search)
		{
			var listcategories = _context.Categories.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				listcategories = listcategories.Where(x => x.CategoryName.Contains(search));
			}
			return listcategories.Count();
		}
		public async Task<Category> GetCategoryById(int id)
		{
			return await _context.Categories.FirstOrDefaultAsync(x => x.CategoryID == id);
		}

		public bool Save()
		{
			var save = _context.SaveChanges();
			return save > 0;
		}

		public bool Update(Category category)
		{
			_context.Update(category);
			return Save();
		}
	}
}
