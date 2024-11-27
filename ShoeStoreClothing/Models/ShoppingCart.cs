using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoopingCartID {  get; set; }    
        public int ProductDetailID {  get; set; }
        [ForeignKey(nameof(ProductDetailID))]  
        public ProductDetail ProductDetail { get; set; }
        public string CustomerID {  get; set; }
        [ForeignKey(nameof(CustomerID))]    
        public AppUser AppUser { get; set; }
        public double Price {  get; set; }  
        public int Quantity {  get; set; }  
    }
}
