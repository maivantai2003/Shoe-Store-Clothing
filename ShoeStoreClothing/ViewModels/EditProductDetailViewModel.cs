using ShoeStoreClothing.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class EditProductDetailViewModel
	{
		public int ProductDetailID { get; set; }
		public int ProductID { get; set; }
		public int ColorID { get; set; }
		public int SizeID { get; set; }
		/*public IFormFile Image {  get; set; }	*/
		public double ProductPrice {  get; set; }
		public double ProductSale { get; set; } = 0d;
		public int StockQuantity { get; set; } = 0;

    }
}
