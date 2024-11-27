using ShoeStoreClothing.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class ProductViewModel
    {
        [Required(ErrorMessage ="Please Choose")]
        public int BrandID { get; set; }
		[Required(ErrorMessage = "Please Choose")]
		public int CategoryID { get; set; }
		[Required(ErrorMessage = "Please Choose")]
		public int MaterialID { get; set; }
        [Required(ErrorMessage ="Please Enter")]
        public string ProductName { get; set; }
    }
}
