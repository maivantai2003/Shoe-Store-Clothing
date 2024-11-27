using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
    public interface IMaterialRepository
    {
        public IEnumerable<Material> GetAllMaterial(string? search, int page = 1);
        public int GetCount(string? search);
        Task<Material> GetMaterialById(int id);
        bool Add(Material material);
        bool Update(Material material);
        bool Delete(Material material);
        bool Save();
    }
}
