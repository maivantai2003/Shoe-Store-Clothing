using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class ProfileViewModel
    {
        public string FullName {  get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email {  get; set; }
        public string PhoneNumber {  get; set; }    
        public string Address {  get; set; }    
    }
}
