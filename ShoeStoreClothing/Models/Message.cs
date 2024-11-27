using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
	public class Message
	{
		[Key]
		public int MessageId {  get; set; }
		public string ReceiverId { get; set; }
		public string SenderId {  get; set; }	
		public DateTime DateTime { get; set; }
		public string content {  get; set; }	
		/*[ForeignKey(nameof(customerId))]
		public AppUser customer { get; set; }
		[ForeignKey(nameof(adminId))]
		public AppUser admin { get; set; }	*/
	}
}
