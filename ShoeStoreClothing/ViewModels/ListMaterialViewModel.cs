using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
    public class ListMaterialViewModel
    {
        public IEnumerable<Material> materials;
        public string search;
        public int TotalPage;
        public int page;
    }
}
