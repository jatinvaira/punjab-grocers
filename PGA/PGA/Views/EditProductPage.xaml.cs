using PGA.Models;
using PGA.Services;
using System;
using System.Net.Http;
using Xamarin.Forms;

namespace PGA.Views
{
    [QueryProperty(nameof(ProductId), nameof(ProductId))]
    public partial class EditProductPage : ContentPage
    {
        private readonly ProductService _productService;
        private int _productId;

        public int ProductId
        {
            get => _productId;
            set
            {
                _productId = value;
                LoadProduct(value);
            }
        }

        public EditProductPage()
        {
            InitializeComponent();

            // Define the IP addresses for emulator and PC
            var emulatorIp = "http://10.0.2.2:5002";
            var pcIp = "http://localhost:5002";

            // Determine the base address to use based on the platform
            var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;

            var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _productService = new ProductService(httpClient);
        }



        private async void LoadProduct(int productId)
        {
            try
            {
                var product = await _productService.GetProductAsync(productId);
                TitleEntry.Text = product.Title;
                DescriptionEntry.Text = product.Description;
                ImageUrlEntry.Text = product.ImageUrl;
                PriceEntry.Text = product.Price.ToString();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                await DisplayAlert("Error", $"Error loading product: {ex.Message}", "OK");
            }
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var product = new Product
            {
                Id = _productId,
                Title = TitleEntry.Text,
                Description = DescriptionEntry.Text,
                ImageUrl = ImageUrlEntry.Text,
                Price = decimal.Parse(PriceEntry.Text)
            };

            try
            {
                await _productService.UpdateProductAsync(product);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                await DisplayAlert("Error", $"Error updating product: {ex.Message}", "OK");
            }
        }
    }
}
