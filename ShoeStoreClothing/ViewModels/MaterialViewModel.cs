using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class MaterialViewModel
    {

        [Required(ErrorMessage ="Please Enter")]
        public string MaterialName { get; set; }
    }
}
