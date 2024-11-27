using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class EditMaterialViewModel
    {
        public int MaterialID { get; set; }
        [Required(ErrorMessage ="Please Enter")]
        public string MaterialName { get; set; }
    }
}
