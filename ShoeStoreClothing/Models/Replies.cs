using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShoeStoreClothing.Models
{
    public class Replies
    {
        [Key]
        public int ReplyId {  get; set; }
        public int CommentId {  get; set; }
        [ForeignKey(nameof(CommentId))]
        public Comment Comment { get; set; }
        public string ReplyContent {  get; set; } =string.Empty;
        public DateTime CreateReply {  get; set; }=DateTime.Now;
        public DateTime UpdateReply { get; set; } = DateTime.Now;
    }
}
