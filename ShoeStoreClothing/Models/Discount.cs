using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
	public class Discount
	{
		[Key]
		public int DiscountId {  get; set; }
		public DateTime DiscountDate { get; set; }
		public string ?DiscountName { get; set; }
		public int Status { get; set; } = 1;
		public ICollection<DiscountDetail>? DiscountDetails { get; set; }
	}
}
