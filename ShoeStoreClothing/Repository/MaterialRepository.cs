using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
    public class MaterialRepository:IMaterialRepository
    {
        private readonly ApplicationDbContext _context;
        public MaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Add(Material material)
        {
            _context.Materials.Add(material);
            return Save();
        }

        public bool Delete(Material material)
        {
            material.Status = 0;
            _context.Update(material);
            return Save();
        }

        public IEnumerable<Material> GetAllMaterial(string? search, int page = 1)
        {
            var listmaterial = _context.Materials.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                listmaterial = listmaterial.Where(x => x.MaterialName.Contains(search));
            }
            var result = PageList<Material>.Create(listmaterial, MyHelper.PAGE, page);
            return result.Select(x => x).ToList();
        }

        public int GetCount(string? search)
        {
            var listmaterial = _context.Materials.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                listmaterial = listmaterial.Where(x => x.MaterialName.Contains(search));
            }
            return listmaterial.Count();
        }

        public async Task<Material> GetMaterialById(int id)
        {
            return await _context.Materials.FirstOrDefaultAsync(x => x.MaterialID == id);
        }

        public bool Save()
        {
            var save=_context.SaveChanges();
            return save > 0;
        }

        public bool Update(Material material)
        {
           _context.Materials.Update(material);
            return Save();
        }
    }
}
