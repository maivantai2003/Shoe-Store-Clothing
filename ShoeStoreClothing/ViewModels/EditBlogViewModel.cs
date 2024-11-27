using System.ComponentModel.DataAnnotations;

namespace ShoeStoreClothing.ViewModels
{
    public class EditBlogViewModel
    {
        public int BlogId { get; set; }
        [Required]
        public string? BlogName { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Subject { get; set; }
    }
}
