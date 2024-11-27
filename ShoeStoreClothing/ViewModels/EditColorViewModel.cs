using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class EditColorViewModel
    {
        public int ColorID { get; set; }
        [Required(ErrorMessage ="Please Enter")]
        public string ColorName { get; set; }
    }
}
