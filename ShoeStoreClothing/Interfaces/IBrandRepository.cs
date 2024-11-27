using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
	public interface IBrandRepository
	{
		public IEnumerable<Brand> GetAllBrand(string? search,int page=1);
		public int GetCount(string? search);
		public Task<Brand> GetBrandById(int id);
		public bool Add(Brand brand);	
		public bool Update(Brand brand);
		public bool Delete(int id);
		public bool Save();
	}
}
