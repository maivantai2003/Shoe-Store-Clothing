using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
	public class BrandRepository : IBrandRepository
	{
		private readonly ApplicationDbContext _context;
		public BrandRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public bool Add(Brand brand)
		{
			_context.Brands.Add(brand);
			return Save();
		}

		public bool Delete(int id)
		{
			var result = _context.Brands.FirstOrDefault(x => x.BrandID == id);
			if (result != null) {
				result.Status = 0;
				_context.Brands.Update(result);
				return Save();
			}
			return false;
		}

		public IEnumerable<Brand> GetAllBrand(string? search, int page = 1)
		{
			var listbrand=_context.Brands.AsQueryable();
			if (!string.IsNullOrEmpty(search)) { 
				listbrand=listbrand.Where(x=>x.BrandName.Contains(search));
			}
			var result = PageList<Brand>.Create(listbrand, MyHelper.PAGE, page);
            return  result.Select(x=>x).ToList();	
		}
		public int GetCount(string? search)
		{
			var listbrand = _context.Brands.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				listbrand = listbrand.Where(x => x.BrandName.Contains(search));
			}
			return listbrand.Count();
		}
        public async Task<Brand> GetBrandById(int id)
		{
			return await _context.Brands.FirstOrDefaultAsync(x => x.BrandID == id);
		}

		public bool Save()
		{
			var save=_context.SaveChanges();
			return save > 0;
		}

		public bool Update(Brand brand)
		{
			_context.Brands.Update(brand);
			return Save();
		}
	}
}
