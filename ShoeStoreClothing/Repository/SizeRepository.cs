using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
	public class SizeRepository : ISizeRepository
	{
		private readonly ApplicationDbContext _context;
		public SizeRepository(ApplicationDbContext context) { 
			_context = context;
		}
		public bool Add(Size size)
		{
			_context.Add(size);
			return Save();
		}

		public bool Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Size> GetAllSize(string? search, int page = 1)
		{
			var listsize = _context.Sizes.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				listsize = listsize.Where(x => x.SizeName.Contains(search));
			}
			var result = PageList<Size>.Create(listsize, MyHelper.PAGE, page);
			return result.Select(x => x).ToList();
		}

		public int GetCount(string? search)
		{
			var listsize = _context.Sizes.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				listsize = listsize.Where(x => x.SizeName.Contains(search));
			}
			return listsize.Count();
		}

		public async Task<Size> GetSizeById(int id)
		{
			return await _context.Sizes.FirstOrDefaultAsync(x => x.SizeID == id);
		}

		public bool Save()
		{
			var save=_context.SaveChanges();
			return save > 0;
		}

		public bool Update(Size size)
		{
			_context.Update(size);
			return Save();
		}
	}
}
