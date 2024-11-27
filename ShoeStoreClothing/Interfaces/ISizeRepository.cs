using ShoeStoreClothing.Models;
namespace ShoeStoreClothing.Interfaces
{
	public interface ISizeRepository
	{
		public IEnumerable<Size> GetAllSize(string? search, int page = 1);
		public int GetCount(string? search);
		public Task<Size> GetSizeById(int id);
		public bool Add(Size size);
		public bool Update(Size size);
		public bool Delete(int id);
		public bool Save();
	}
}
