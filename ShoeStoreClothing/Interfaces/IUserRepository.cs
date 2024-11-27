using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.Interfaces
{
	public interface IUserRepository
	{
		public IEnumerable<AppUser> GetAllUser(string? search, int page = 1);
		public int GetCount(string? search);
		public Task<AppUser> GetUserById(int id);
		public Task<bool> Add(AppUser user);
		public Task<bool> Update(AppUser user);
		public Task<bool> Delete(int id);
		public Task<bool> Save(AppUser appUser,string password);
	}
}
