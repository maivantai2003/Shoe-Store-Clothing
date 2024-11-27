using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
	public class ListUserViewModel
	{
		public IEnumerable<AppUser> Users;
		public string search;
		public int TotalPage;
		public int page;
	}
}
