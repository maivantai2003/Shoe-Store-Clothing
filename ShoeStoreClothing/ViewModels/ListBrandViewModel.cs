using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
    public class ListBrandViewModel
    {
        public IEnumerable<Brand> brands = new List<Brand>();
        public int TotalPage;
        public string? search;
        public int page;

    }
}
