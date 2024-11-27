using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
	public interface ISupplierRepository
	{
		public IEnumerable<Supplier> GetAllSupplier(string? search, int page = 1);
		public Task<Supplier> GetSupplierById(int id);
		public int GetCount(string? search);
		public bool Update(Supplier supplier);
		public bool Delete(Supplier supplier);	
		public bool Add(Supplier supplier);
		public bool Save();
	}
}
