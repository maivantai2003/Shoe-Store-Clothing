using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace ShoeStoreClothing.Models
{
    public class AppUser:IdentityUser
    {
        public string FullName { get; set; }
        public string? PhoneNumber { get; set; }=string.Empty;
        public string? Address { get; set; }=string.Empty ;
        public int Status { get; set; } = 1;
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }  
        public ICollection<Message> messages { get; set; }
        public ICollection<Notifications> Notifications { get; set; }
        [JsonIgnore]
        public ICollection<Comment>? Comments { get; set; }  
    }
}
