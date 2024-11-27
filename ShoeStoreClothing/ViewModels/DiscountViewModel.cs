using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class DiscountViewModel
    {
        [Required]
        public DateTime DiscountDate { get; set; }
        [Required]
        public string? DiscountName { get; set; }
    }
}
