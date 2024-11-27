using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewComponents
{
    public class NumberCartViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public NumberCartViewComponent(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customer = await _userManager.GetUserAsync(UserClaimsPrincipal);
            var customerID=customer?.Id;
            if (customerID == null) {
                return View("Number", 0);
            }
            int result = _context.ShoppingCarts.Where(x => x.CustomerID == customer.Id).Count();
            return View("Number",result);
        }
    }
}
