using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListProductDetailViewModel
	{
		public IEnumerable<ProductDetail> productDetails { get; set; }
		public string search { get; set; }
		public int TotalPage{get; set;}
		public int page {  get; set;}
		public string action {  get; set; }	
	}
}
