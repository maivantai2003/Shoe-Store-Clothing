using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
    public interface IProductRepository
    {
        public IEnumerable<Product> GetAllProduct(string search, int page = 1);
        public int GetCount(string? search);
        public Task<Product> GetProductById(int id);
        public bool Add(Product product);
        public bool Update(Product product);
        public bool Delete(int id);
        public bool Save();
    }
}
