using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
    public interface ICommentRepository
    {
        public IEnumerable<Comment> GetAllComment(string userId, string search, int page = 1);
        public IEnumerable<Comment> GetAllCommentProduct(int productId,int page = 1);
        public IEnumerable<Comment> GetAllCommentProduct(int productDetailId);
        public int GetCount(string userId, string? search);
        public Task<Comment> GetCommentById(int id);
        public bool Add(Comment comment);
        public bool Update(Comment comment);
        public bool Delete(int id);
        public bool Save();
    }
}
