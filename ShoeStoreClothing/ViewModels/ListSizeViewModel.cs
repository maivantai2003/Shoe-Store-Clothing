using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListSizeViewModel
	{
		public IEnumerable<Size> Sizes { get; set; }
		public string search { get; set; }
		public int TotalPage {  get; set; }
		public int page { get; set; }	
	}
}
