//using Xamarin.Forms;
//using PGA.ViewModels;
//using System;

//namespace PGA.Views
//{
//    public partial class CartPage : ContentPage
//    {
//        private CartViewModel _viewModel;

//        public CartPage()
//        {
//            InitializeComponent();
//            _viewModel = new CartViewModel();
//            BindingContext = _viewModel;
//        }

//        private async void OnCheckoutClicked(object sender, EventArgs e)
//        {
//            // Navigate to checkout page
//            await Navigation.PushAsync(new CheckoutPage());
//        }
//    }
//}

using PGA.Services;
using System;
using PGA.Models;
using Xamarin.Forms;

namespace PGA.Views
{
    public partial class CartPage : ContentPage
    {
        public CartPage()
        {
            InitializeComponent();
            // Bind the ListView to the cart items
            cartListView.ItemsSource = CartService.CartItems;
        }

        private void OnRemoveItemClicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var product = menuItem?.BindingContext as Product;
            if (product != null)
            {
                CartService.CartItems.Remove(product);
                cartListView.ItemsSource = null;
                cartListView.ItemsSource = CartService.CartItems;
            }
        }

        private async void OnCheckoutClicked(object sender, EventArgs e)
        {
            // Navigate to the Checkout Page
            await Navigation.PushAsync(new CheckoutPage());
        }
    }
}
