using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class SupplierViewModel
	{
		[Required(ErrorMessage ="Please Enter")]
		public string SupplierName { get; set; }
		[Required(ErrorMessage = "Please Enter")]
		public string Address { get; set; }
		[Required(ErrorMessage = "Please Enter")]
		[MaxLength(10,ErrorMessage ="Please Enter Matched")]
		public string PhoneNumber { get; set; }
	}
}
