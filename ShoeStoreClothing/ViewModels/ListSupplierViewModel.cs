using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListSupplierViewModel
	{
		public IEnumerable<Supplier> Suppliers { get; set; }
		public string search;
		public int page;
		public int TotalPage;
	}
}
