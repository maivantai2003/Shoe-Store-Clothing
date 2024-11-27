using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
	public class DiscountDetail
	{
		[Key]
		public int DiscountDetailId {  get; set; }
		public int DiscountId {  get; set; }
		public int ProductDetailId {  get; set; }
		public double PriceDiscount {  get; set; }
		public int Status { get; set; } = 1;
		[ForeignKey(nameof(DiscountDetailId))]
		public ProductDetail? ProductDetail { get; set; }
		[ForeignKey(nameof(DiscountId))]
		public Discount? Discount { get; set; }	
	}
}
