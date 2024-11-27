using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class Comment
    {
        [Key]
        public int CommentId {get; set;}
        public string UserId {get; set;}
        [ForeignKey(nameof(UserId))]
        public AppUser User {get; set;}
        public string CommentText {get; set;}=string.Empty;
        public DateTime CreateComment {get; set;}= DateTime.Now;
        public DateTime UpdateComment {get; set;}=DateTime.Now;
        public int ProductDetailId { get; set;}
        [ForeignKey(nameof(ProductDetailId))]
        public ProductDetail ProductDetail {  get; set;}
        public ICollection<Replies> replies {get; set;} 
    }
}
