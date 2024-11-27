using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
	public class Blog
	{
		[Key]
		public int BlogId { get; set; }
		public string? BlogName { get; set; }
		public string? Title {  get; set; }	
		public string? Subject { get; set; }
		public int Status { get; set; } = 1;

	}
}
