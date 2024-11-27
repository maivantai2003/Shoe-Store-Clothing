using Microsoft.AspNetCore.Mvc;

namespace ShoeStoreClothing.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult contact()
        {
            return View();
        }
    }
}
