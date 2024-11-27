using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListBlogViewModel
	{
		public IEnumerable<Blog> blogs = new List<Blog>();
		public int TotalPage;
		public string? search;
		public int page;
	}
}
