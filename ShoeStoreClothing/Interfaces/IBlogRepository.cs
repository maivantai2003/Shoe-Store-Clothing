using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
	public interface IBlogRepository
	{
		public IEnumerable<Blog> GetAllBlog(string? search, int page = 1);
		public int GetCount(string? search);
		public Task<Blog> GetBlogById(int id);
		public bool Add(Blog blog);
		public bool Update(Blog blog);
		public bool Delete(int id);
		public bool Save();
	}
}
