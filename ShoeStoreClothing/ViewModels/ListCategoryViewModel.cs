using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListCategoryViewModel
	{
		public IEnumerable<Category> categories { get; set; }
		public int TotalPage;
		public string search;
		public int page;
	}
}
