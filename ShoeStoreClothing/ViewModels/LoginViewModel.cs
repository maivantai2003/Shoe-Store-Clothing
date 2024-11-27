using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }    
        public string? returnUrl {  get; set; }  
    }
}
