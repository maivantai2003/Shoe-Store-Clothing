using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class FavoriteProduct
    {
        [Key]
        public int FavoriteId { get; set; }
        public string CustomerID { get; set; }
        [ForeignKey(nameof(CustomerID))]
        public AppUser User { get; set; }
        public int ProductDetailID { get; set; }
        [ForeignKey(nameof(ProductDetailID))]
        public ProductDetail ProductDetail { get; set; }    
    }
}
