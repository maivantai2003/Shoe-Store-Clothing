using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
    public class CommentRepository(ApplicationDbContext _context) : ICommentRepository
    {
        public bool Add(Comment comment)
        {
            _context.Comments.Add(comment);
            return Save();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAllComment(string userId,string search, int page = 1)
        {
            var query = _context.Comments.Include(x => x.ProductDetail).Include(x => x.ProductDetail.Size).Include(x => x.ProductDetail.Product).Include(x=>x.ProductDetail.Color).Where(x => x.UserId == userId).AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.CommentText.Contains(search));
            }
            var result = PageList<Comment>.Create(query, MyHelper.PAGE, page);
            return result.Select(x => x).ToList();
        }

        public int GetCount(string userId, string? search)
        {
            var query = _context.Comments.Include(x=>x.ProductDetail).Include(x=>x.ProductDetail.Size).Include(x=>x.ProductDetail.Product).Include(x => x.ProductDetail.Color).Where(x => x.UserId == userId).AsQueryable().AsNoTracking();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.CommentText.Contains(search));
            }
            return query.Count();
        }

        public async Task<Comment> GetCommentById(int id)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.CommentId == id)??new Comment();
        }

        public bool Save()
        {
            var save=_context.SaveChanges();
            return save > 0;
        }

        public bool Update(Comment comment)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Comment> GetAllCommentProduct(int productId,int page = 1)
        {
            var query = _context.Comments.Include(x => x.ProductDetail).Include(x=>x.User).Include(x => x.ProductDetail.Product).Where(x=>x.ProductDetailId==productId).AsQueryable().AsNoTracking();
            var result = PageList<Comment>.Create(query, MyHelper.PAGE, page);
            return result.Select(x => x).ToList();
        }
        public IEnumerable<Comment> GetAllCommentProduct(int productDetailId)
        {
            //var query = _context.Comments.Include(x => x.ProductDetail).Include(x => x.User).Include(x => x.ProductDetail.Product).AsQueryable().AsNoTracking();
            //var result = PageList<Comment>.Create(query, MyHelper.PAGE, page);
            var query = _context.Comments.Include(x => x.User).Where(x => x.ProductDetailId == productDetailId).AsNoTracking();
            return query.Select(x => x).ToList();
        }
    }
}
