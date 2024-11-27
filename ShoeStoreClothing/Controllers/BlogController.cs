using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ShoeStoreClothing.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult blog()
        {
            return View();
        }
        public IActionResult singleblog()
        {
            return View();
        }
        [Route("/regular-page")]
        public IActionResult regularpage()
        {
            return View();
        }
    }
}
