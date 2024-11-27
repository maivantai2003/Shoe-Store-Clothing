using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class BrandViewModel
	{
        [Required(ErrorMessage = "Brand name is required")]
		public string? BrandName { get; set; }
	}
}
