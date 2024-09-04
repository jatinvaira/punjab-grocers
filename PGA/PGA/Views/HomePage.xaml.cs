using PGA.Models;
using PGA.Services;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;

namespace PGA.Views
{
    public partial class HomePage : ContentPage
    {
        private readonly ProductService _productService;
        private readonly CheckoutService _checkoutService;

        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Checkout> Purchases { get; set; }

        public HomePage()
        {
            InitializeComponent();
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5002") };
            _productService = new ProductService(httpClient);
            _checkoutService = new CheckoutService(); // Instantiate CheckoutService without arguments
            Products = new ObservableCollection<Product>();
            Purchases = new ObservableCollection<Checkout>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            try
            {
                var products = await _productService.GetProductsAsync();
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }

                var purchases = await _checkoutService.GetCheckoutsAsync();
                Purchases.Clear();
                foreach (var purchase in purchases)
                {
                    Purchases.Add(purchase);
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading data: {ex.Message}", "OK");
            }
        }
        private async void OnUpdateProductClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var productId = (int)button.CommandParameter;

            try
            {
                var product = await _productService.GetProductAsync(productId);
                if (product != null)
                {
                    await Navigation.PushAsync(new UpdateProductPage(product));
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error loading product: {ex.Message}", "OK");
            }
        }
        private async void OnAddProductClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddProductPage());
        }
        private async void OnDeleteProductClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var purchaseId = (int)button.CommandParameter;
            try
            {
                await _checkoutService.DeleteCheckoutAsync(purchaseId);
                Purchases.Remove(Purchases.First(p => p.Id == purchaseId));
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Error deleting purchase: {ex.Message}", "OK");
            }
        }
    }
}
