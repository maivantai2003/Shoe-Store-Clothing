using ShoeStoreClothing.Models;

namespace ShoeStoreClothing.ViewModels
{
    public class ListShoppingCartViewModel
    {
        public IEnumerable<ShoppingCart> shoppingCarts { get; set; }
        public double TotalPrice { get; set; }
        public double Discount { get; set; } = 0;
    }
}
