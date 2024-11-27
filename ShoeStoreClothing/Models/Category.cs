using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int Status { get; set; } = 1;
        public ICollection<Product> Products { get; set; }  
    }
}
