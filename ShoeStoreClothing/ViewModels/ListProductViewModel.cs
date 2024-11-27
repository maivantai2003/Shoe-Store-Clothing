using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListProductViewModel
	{
		public IEnumerable<Product>? Products { get; set; }
		public string search;
		public int TotalPage;
		public int page;
	}
}
