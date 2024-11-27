using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class Notifications
    {
        [Key]
        public int NotificationId {  get; set; }
        public string UserId {  get; set; }
        [ForeignKey(nameof(UserId))] 
        public AppUser AppUser { get; set; }
        public DateTime dateTimeSend { get; set; }= DateTime.Now;
        public string notificationContent {  get; set; }
        public int Status { get; set; } = 1;
    }
}
