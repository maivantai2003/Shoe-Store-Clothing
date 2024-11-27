using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class ColorViewModel
    {
        [Required(ErrorMessage ="Please Enter")]
        public string ColorName {  get; set; }  
    }
}
