using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
    public interface IProductDetailRepository
    {
        public IEnumerable<ProductDetail> GetAllProductDetail(string search, int page = 1);
        public int GetCount(string? search);
        public Task<ProductDetail> GetProductDetailById(int id);
        public bool Add(ProductDetail productDetail);
        public bool Update(ProductDetail productDetail);
        public bool Delete(int id);
        public bool Save();
    }
}
