using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
	public interface ICategoryRepository
	{
		public IEnumerable<Category> GetAllCategories(string? search, int page = 1);
		public int GetCount(string? search);
		Task<Category> GetCategoryById(int id);
		bool Add(Category category);
		bool Update(Category category);
		bool Delete(Category category);
		bool Save();
	}
}
