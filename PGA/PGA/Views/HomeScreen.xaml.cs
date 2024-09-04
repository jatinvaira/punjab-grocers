using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using PGA.Models;
using PGA.Services;
using System.Net.Http;

namespace PGA.Views
{
    public partial class HomeScreen : ContentPage
    {
        private readonly ProductService _productsService;
        public ObservableCollection<Product> Products { get; set; }
        public ICommand AddToCartCommand { get; }

        public HomeScreen()
        {
            InitializeComponent();

            _productsService = new ProductService(new HttpClient());

            Products = new ObservableCollection<Product>();
            AddToCartCommand = new Command<Product>(AddToCart);

            LoadProducts();
            productsListView.ItemsSource = Products;
        }

        private async void LoadProducts()
        {
            var products = await _productsService.GetProductsAsync();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }

        private async void AddToCart(Product product)
        {
            // Add the product to the cart using the CartService
            CartService.AddProductToCart(product);

            // Display a confirmation message
            await DisplayAlert("Added to Cart", $"{product.Title} has been added to your cart.", "OK");

            // Navigate to the cart page
            await Navigation.PushAsync(new CartPage());
        }

        private async void OnGoToHomePageClicked(object sender, EventArgs e)
        {
            // Navigate to Home Page
            await Navigation.PushAsync(new HomePage());
        }

        private async void OnProductSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Product product)
            {
                // Navigate to product detail page with the Product object
                await Navigation.PushAsync(new ProductDetailPage(product));
            }
        }
        private void OnAddToCartClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            var product = button?.BindingContext as Product;

            if (product != null)
            {
                CartService.AddProductToCart(product);
                DisplayAlert("Cart", $"{product.Title} has been added to your cart.", "OK");
                Navigation.PushAsync(new CartPage());
            }
        }
    }
}
