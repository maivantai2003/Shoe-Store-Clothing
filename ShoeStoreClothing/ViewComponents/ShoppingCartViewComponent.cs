using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShoeStoreClothing.Data;
using ShoeStoreClothing.Models;
using ShoeStoreClothing.ViewModels;

namespace ShoeStoreClothing.ViewComponents
{
    public class ShoppingCartViewComponent:ViewComponent
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ShoppingCartViewComponent(ApplicationDbContext context,UserManager<AppUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var customer = await _userManager.GetUserAsync(UserClaimsPrincipal);
            if (customer == null) {
                return View("ShoppingCart",new ListShoppingCartViewModel() { });
            }
            var customerID = customer?.Id;
            var listShoppingCart = _context.ShoppingCarts.Where(x => x.CustomerID == customerID).Include(x => x.ProductDetail).ThenInclude(x=>x.Product).ThenInclude(x=>x.Brand).Include(x=>x.ProductDetail.Color).Include(x=>x.ProductDetail.Size);
            var shoppingCart = new ListShoppingCartViewModel()
            {
                shoppingCarts = listShoppingCart.ToList(),
                TotalPrice = listShoppingCart.Sum(x => x.Price*x.Quantity),
                Discount = listShoppingCart.Sum(x => x.ProductDetail.PriceSale*x.Quantity)
            };
            return View("ShoppingCart",shoppingCart);
        }
    }
}
