using Microsoft.AspNetCore.Mvc;

namespace ShoeStoreClothing.Controllers
{
	public class FavoriteProductController : Controller
	{
		[Route("/FavoriteProduct")]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Favorite(int id)
		{
			return Json(new { id });
		}

    }
}
