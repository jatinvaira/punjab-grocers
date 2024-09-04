using System.Collections.Generic;
using System.Collections.ObjectModel;
using PGA.Models;

namespace PGA.Services
{
    public static class CartService
    {
        private static readonly List<Product> _cartItems = new List<Product>();
        public static List<Product> CartItems => _cartItems;

        public static void AddProductToCart(Product product)
        {
            if (product != null)
            {
                _cartItems.Add(product);
            }
        }

        public static List<Product> GetCartItems()
        {
            return _cartItems;
        }

        public static void RemoveProductFromCart(Product product)
        {
            if (product != null)
            {
                _cartItems.Remove(product);
            }
        }

        public static void ClearCart()
        {
            _cartItems.Clear();
        }
    }

}
