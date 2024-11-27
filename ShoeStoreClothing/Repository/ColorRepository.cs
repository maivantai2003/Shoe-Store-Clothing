using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
    public class ColorRepository : IColorRepository
    {
        private readonly ApplicationDbContext _context;
        public ColorRepository(ApplicationDbContext context) { 
            _context = context; 
        }
        public bool Add(Color color)
        {
            _context.Add(color);
            return Save();
        }

        public bool Delete(int id)
        {
            var result = _context.Colors.FirstOrDefault(x=>x.ColorID== id);
            if (result != null) {
                result.Status = 0;
                _context.Update(result);
                return Save();
            }
            return false;
        }

        public IEnumerable<Color> GetAllColor(string? search, int page = 1)
        {
            var listcolor = _context.Colors.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                listcolor = listcolor.Where(x => x.ColorName.Contains(search));
            }
            var result = PageList<Color>.Create(listcolor, MyHelper.PAGE, page);
            return result.Select(x => x).ToList();
        }

        public async Task<Color> GetColorById(int id)
        {
            return await _context.Colors.FirstOrDefaultAsync(x => x.ColorID == id);
        }

        public int GetCount(string? search)
        {
            var listcolor = _context.Colors.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                listcolor = listcolor.Where(x => x.ColorName.Contains(search));
            }
            return listcolor.Count();
        }

        public bool Save()
        {
            var save=_context.SaveChanges();
            return save > 0;
        }

        public bool Update(Color color)
        {
            _context.Colors.Update(color);
            return Save();
        }
    }
}
