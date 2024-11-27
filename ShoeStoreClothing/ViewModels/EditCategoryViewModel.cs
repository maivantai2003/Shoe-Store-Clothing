using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class EditCategoryViewModel
	{
		public int CategoryID { get; set; }
		[Required(ErrorMessage ="Please Enter")]	
		
		public string CategoryName { get; set; }
	}
}
