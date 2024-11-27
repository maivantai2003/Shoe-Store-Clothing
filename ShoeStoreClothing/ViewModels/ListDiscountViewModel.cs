using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListDiscountViewModel
	{
		public IEnumerable<Discount> Discounts { get; set; }
		public string search;
		public int TotalPage;
		public int page;
	}
}
