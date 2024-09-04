using PGA.Models;
using PGA.Services;
using System;
using System.Linq;
using Xamarin.Forms;

namespace PGA.Views
{
    public partial class CheckoutPage : ContentPage
    {
        private readonly CheckoutService _checkoutService;

        public CheckoutPage()
        {
            InitializeComponent();
            checkoutListView.ItemsSource = CartService.CartItems;

            // Initialize CheckoutService
            _checkoutService = new CheckoutService();

            // Calculate TotalPrice
            decimal totalPrice = 0;
            foreach (var item in CartService.CartItems)
            {
                totalPrice += item.Price;
            }

            // Set the TotalPrice on the UI if needed
            totalPriceLabel.Text = $"Total: {totalPrice:C}";
        }

        private async void OnPlaceOrderClicked(object sender, EventArgs e)
        {
            // Create the checkout object
            var checkout = new Checkout
            {
                Name = nameEntry.Text,
                Address = addressEntry.Text,
                PaymentInfo = paymentInfoEntry.Text,
                TotalPrice = CartService.CartItems.Sum(item => item.Price),
                OrderDate = DateTime.UtcNow // Set the order date
            };

            // Call the API to store the checkout data
            var result = await _checkoutService.CreateCheckoutAsync(checkout);

            if (result != null)
            {
                // Clear the cart
                CartService.ClearCart();

                // Navigate to Success Page
                await Navigation.PushAsync(new SuccessPage());
            }
            else
            {
                await DisplayAlert("Error", "Failed to place order. Please try again.", "OK");
            }
        }
    }
}
