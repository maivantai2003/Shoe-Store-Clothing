using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int InvoiceDetailId { get; set; }
        public int InvoiceID { get; set; }
        [ForeignKey(nameof(InvoiceID))]
        public Invoice Invoice { get; set; }
        public int ProductDetailID { get; set; }
        [ForeignKey(nameof(ProductDetailID))]   
        public ProductDetail ProductDetail { get; set; }
        public double ProductPrice { get; set; } = 0d;
        public int Quantity { get; set; } = 0;
        public double Discount { get; set; } = 0d;
        public double TotalPrice { get; set; } = 0d;
    }
}
