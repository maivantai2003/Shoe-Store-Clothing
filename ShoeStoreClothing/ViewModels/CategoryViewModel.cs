using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class CategoryViewModel
	{
		[Required(ErrorMessage = "Please Enter")]
		public string CategoryName { get; set; }
	}
}
