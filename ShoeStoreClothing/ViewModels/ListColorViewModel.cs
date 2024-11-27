using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
    public class ListColorViewModel
    {
        public IEnumerable<Color> colors;
        public int TotalPage;
        public string search;
        public int page;
    }
}
