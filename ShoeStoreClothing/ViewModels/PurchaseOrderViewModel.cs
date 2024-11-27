using ShoeStoreClothing.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
	public class PurchaseOrderViewModel
	{
		public int SupplierID { get; set; }
		public double TotalAmount { get; set; }
		public int TotalQuantity { get; set; }
	}
}
