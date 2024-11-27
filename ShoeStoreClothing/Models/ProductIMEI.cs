using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class ProductIMEI
    {
        [Key]
        public int IMEICode { get; set; }
        public int ProductDetailID { get; set; }
        [ForeignKey(nameof(ProductDetailID))]
        public ProductDetail ProductDetail { get; set; }
        public int Status { get; set; } = 1;
    }
}
