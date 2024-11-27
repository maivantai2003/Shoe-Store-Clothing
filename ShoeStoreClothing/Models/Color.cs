using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
    public class Color
    {
        [Key]
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public int Status { get; set; } = 1;
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
