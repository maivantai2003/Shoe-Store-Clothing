using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListInvoiceViewModel
	{
		public IEnumerable<Invoice> invoices { get; set; }
		public string search {  get; set; }	
		public int TotalPage { get; set; }
		public int page { get; set; }
		public string action {  get; set; }	
	}
}
