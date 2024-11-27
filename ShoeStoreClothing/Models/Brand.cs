namespace ShoeStoreClothing.Models
{
    public class Brand
    {
        public int BrandID { get; set; }
        public string BrandName { get; set; }
        public int Status { get; set; } = 1;
        public ICollection<Product> Products { get; set; }
    }
}
