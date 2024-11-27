using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class SizeViewModel
	{
		[Required(ErrorMessage ="Please Enter")]
		public string SizeName {  get; set; }	
	}
}
