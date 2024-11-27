using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
    public interface IColorRepository
    {
        public IEnumerable<Color> GetAllColor(string? search, int page = 1);
        public int GetCount(string? search);
        public Task<Color> GetColorById(int id);
        public bool Add(Color color);
        public bool Update(Color color);
        public bool Delete(int id);
        public bool Save();
    }
}
