using PGA.Models;
using PGA.Services;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddProductPage : ContentPage
    {
        private readonly ProductService _productService;

        public AddProductPage()
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

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var product = new Product
            {
                Title = TitleEntry.Text,
                Description = DescriptionEntry.Text,
                ImageUrl = ImageUrlEntry.Text,
                Price = decimal.Parse(PriceEntry.Text)
            };

            try
            {
                await _productService.AddProductAsync(product);
                await Navigation.PopAsync();
            }
            catch (Exception ex)
            {
                // Handle or log the exception
                await DisplayAlert("Error", $"Error adding product: {ex.Message}", "OK");
            }
        }
    }
}
