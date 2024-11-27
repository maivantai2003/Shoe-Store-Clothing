using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public int BrandID { get; set; }
        [ForeignKey(nameof(BrandID))]
        public Brand Brand { get; set; }
        public int CategoryID { get; set; }
        [ForeignKey(nameof(CategoryID))]
        public Category Category { get; set; }
        public int MaterialID { get; set; }
        [ForeignKey(nameof(MaterialID))]
        public Material Material { get; set; }
        public string ProductName { get; set; }
        //public int StockQuantity { get; set; } = 0;
        public int Status { get; set; } = 1;
        public ICollection<ProductDetail> ProductDetails { get; set; }  
    }
}
