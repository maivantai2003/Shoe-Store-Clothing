using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class UpdateBrandViewModel
    {
        [Required]
        public int BrandID { get; set; }
        [Required(ErrorMessage ="Please enter")]
        public string BrandName { get; set; }
    }
}
