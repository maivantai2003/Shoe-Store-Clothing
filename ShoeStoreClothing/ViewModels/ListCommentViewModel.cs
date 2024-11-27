using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
    public class ListCommentViewModel
    {
        public IEnumerable<Comment> comments { get; set; }
        public int TotalPage;
        public string search;
        public int page;
    }
}
