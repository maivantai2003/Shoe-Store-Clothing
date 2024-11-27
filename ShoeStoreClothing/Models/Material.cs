using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
    public class Material
    {
        [Key]
        public int MaterialID { get; set; }
        public string MaterialName { get; set; }
        public int Status { get; set; } = 1;
        public ICollection<Product> Products { get; set; }
    }
}
