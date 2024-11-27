using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
	public class BlogRepository : IBlogRepository
	{
		private readonly ApplicationDbContext _context;
		public BlogRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public bool Add(Blog blog)
		{
			_context.Blogs.Add(blog);
			return Save();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Blog> GetAllBlog(string? search, int page = 1)
		{
			var query=_context.Blogs.AsQueryable().AsNoTracking();
			if (!string.IsNullOrEmpty(search))
			{
				query=query.Where(x=>x.BlogName.Contains(search) || x.Title.Contains(search) || x.Subject.Contains(search));
			}
			var result = PageList<Blog>.Create(query, MyHelper.PAGE, page);
			return result.Select(x => x).ToList();
		}

		public async Task<Blog> GetBlogById(int id)
		{
			return await _context.Blogs.FirstOrDefaultAsync(x => x.BlogId == id) ?? new Blog();
		}

		public int GetCount(string? search)
		{
			var query = _context.Blogs.AsQueryable().AsNoTracking();
			if (!string.IsNullOrEmpty(search))
			{
				query = query.Where(x => x.BlogName.Contains(search) || x.Title.Contains(search) || x.Subject.Contains(search));
			}
			return query.Count();	
		}

		public bool Save()
		{
			var save = _context.SaveChanges();
			return save > 0;
		}

		public bool Update(Blog blog)
		{
			_context.Blogs.Add(blog);
			return Save();
		}
	}
}
