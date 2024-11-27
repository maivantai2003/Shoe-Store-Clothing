using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class PurchaseOrderDetail
    {
        [Key]
        public int PurchaseOrderDetailID {  get; set; } 
        public int PurchaseOrderID { get; set; }
        [ForeignKey(nameof(PurchaseOrderID))]
        public PurchaseOrder purchaseOrder { get; set; }
        public int ProductDetailID { get; set; }
        [ForeignKey(nameof(ProductDetailID))]
        public ProductDetail productDetail { get; set; }    
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public double PurchasePrice { get; set; }
        public double TotalPrice=>Quantity*PurchasePrice;
    }
}
