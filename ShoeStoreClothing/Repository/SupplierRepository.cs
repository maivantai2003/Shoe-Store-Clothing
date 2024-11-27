using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Helpers;
using ShoeStoreClothing.Interfaces;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Repository
{
	public class SupplierRepository : ISupplierRepository
	{
		private readonly ApplicationDbContext _context;	
		public SupplierRepository(ApplicationDbContext context)
		{
			_context = context;
		}
		public bool Add(Supplier supplier)
		{
			_context.Add(supplier);
			return Save();
		}

		public bool Delete(Supplier supplier)
		{
			_context.Remove(supplier);
			return Save();
		}

		public IEnumerable<Supplier> GetAllSupplier(string? search,int page=1)
		{
			var listsupplier = _context.Suppliers.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				listsupplier = listsupplier.Where(x => x.SupplierName.Contains(search) || x.Address.Contains(search) || x.PhoneNumber.Contains(search));
			}
			var result = PageList<Supplier>.Create(listsupplier, MyHelper.PAGE, page);
			return result.Select(x => x).ToList();
		}
		public int GetCount(string? search)
		{
			var listsupplier = _context.Suppliers.AsQueryable();
			if (!string.IsNullOrEmpty(search))
			{
				listsupplier = listsupplier.Where(x => x.SupplierName.Contains(search) || x.Address.Contains(search) || x.PhoneNumber.Contains(search));
			}
			return listsupplier.Count();
		}
		public async Task<Supplier> GetSupplierById(int id)
		{
			return await _context.Suppliers.FirstOrDefaultAsync(x => x.SupplierID == id);

		}

		public bool Save()
		{
			var save=_context.SaveChanges();
			return save > 0;
		}

		public bool Update(Supplier supplier)
		{
			_context.Suppliers.Update(supplier);
			return Save();
		}
	}
}
