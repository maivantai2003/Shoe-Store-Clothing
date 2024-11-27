using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
    public class Size
    {
        [Key]
        public int SizeID { get; set; }
        public string SizeName { get; set; }
        public int Status { get; set; } = 1;
        public ICollection<ProductDetail> ProductDetails { get; set; }
    }
}
