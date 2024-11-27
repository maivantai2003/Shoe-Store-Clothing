using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
    public class ProductRepository:IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Product product)
        {
            _context.Products.Add(product);
            return Save();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAllProduct(string search,int page=1)
        {
            var listproduct = _context.Products.Include(x=>x.Brand).Include(x=>x.Category).Include(x=>x.Material).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                listproduct = listproduct.Where(x => x.ProductName.Contains(search));
            }
            var result = PageList<Product>.Create(listproduct, MyHelper.PAGE, page);
            return result.Select(x => x).ToList();
        }

        public int GetCount(string? search)
        {
			var listproduct = _context.Products.Include(x => x.Brand).Include(x => x.Category).Include(x => x.Material).AsQueryable();
			if (!string.IsNullOrEmpty(search))
            {
                listproduct = listproduct.Where(x => x.ProductName.Contains(search));
            }
            return listproduct.Count();
        }

        public async Task<Product> GetProductById(int id)
        {
            return await _context.Products.FirstOrDefaultAsync(x => x.ProductID == id);
        }

        public bool Save()
        {
            var save=_context.SaveChanges();
            return save > 0;
        }

        public bool Update(Product size)
        {
            _context.Update(size);
            return Save();
        }
    }
}
