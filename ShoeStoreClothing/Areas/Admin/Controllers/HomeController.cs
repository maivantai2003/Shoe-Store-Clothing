using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoeStoreClothing.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles ="Admin,Employee")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
