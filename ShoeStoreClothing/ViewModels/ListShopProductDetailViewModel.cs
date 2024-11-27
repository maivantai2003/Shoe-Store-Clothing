using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
    public class ListShopProductDetailViewModel
    {
        public IEnumerable<ProductDetail> productDetails { get; set; }
        public string? search { get; set; }
        public string? brand { get; set; }
        public string? category { get; set; }   
        public int TotalPage {  get; set; }
        public int page { get; set; }  
        public int TotalRecord { get; set; }
    }
}
