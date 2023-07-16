using PieShop.Models;

namespace PieShop.ViewModels
{
    public class ShoppingCartViewModel
    {
        public IShoppingCart ShoppingCart { get; }
        public decimal ShoppingCartTotal { get; set; }
        public ShoppingCartViewModel(IShoppingCart shoppingCart, decimal shoppingCartTotal) 
        {
            ShoppingCart = shoppingCart;
            ShoppingCartTotal = shoppingCartTotal;
        }
    }
}
