using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
    public class ProductDetailRepository:IProductDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(ProductDetail productDetail)
        {
            _context.Add(productDetail);
            return Save();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProductDetail> GetAllProductDetail(string search, int page = 1)
        {
            var listProductDetail = _context.ProductDetails.Include(p=>p.Product).Include(p=>p.Color).Include(p=>p.Size).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                listProductDetail = listProductDetail.Where(x =>x.ProductID.ToString().Contains(search));
            }
            var result = PageList<ProductDetail>.Create(listProductDetail, MyHelper.PAGE, page);
            return result.Select(x => x).ToList();
        }

        public int GetCount(string? search)
        {
            var listProductDetail = _context.ProductDetails.Include(p => p.Product).Include(p => p.Color).Include(p => p.Size).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                listProductDetail = listProductDetail.Where(x => x.ProductID.ToString().Contains(search));
            }
            return listProductDetail.Count();   
        }

        public async Task<ProductDetail> GetProductDetailById(int id)
        {
            return await _context.ProductDetails.Include(p=>p.Product).FirstOrDefaultAsync(x => x.ProductDetailID == id);
        }

        public bool Save()
        {
            var save=_context.SaveChanges();
            return save > 0;
        }

        public bool Update(ProductDetail productDetail)
        {
            _context.Update(productDetail);
            return Save();
        }
    }
}
