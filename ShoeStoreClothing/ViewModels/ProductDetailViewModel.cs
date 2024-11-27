using ShoeStoreClothing.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class ProductDetailViewModel
	{
		public int ProductID { get; set; }
		public int ColorID { get; set; }
		public int SizeID { get; set; }
		public IFormFile Image { get; set; }
		public int StockQuantity { get; set; } = 0;
		public double ProductPrice { get; set; } = 0d;
	}
}
