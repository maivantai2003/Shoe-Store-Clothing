namespace ShoeStoreClothing.ViewModels
{
    public class CommentViewModel
    {
        public string UserId {  get; set; } 
        public int ProductDetailId {  get; set; }
        public string CommentText { get; set; } = string.Empty;
    }
}
