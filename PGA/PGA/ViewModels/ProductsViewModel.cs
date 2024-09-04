using System.Collections.ObjectModel;
using System.Threading.Tasks;
using PGA.Models;
using PGA.Services;
using System.Net.Http;
using System;
using Xamarin.Forms;

namespace PGA.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        public ObservableCollection<Product> Products { get; set; }

        public ProductsViewModel()
        {
            // Define the IP addresses for emulator and PC
            var emulatorIp = "http://10.0.2.2:5002";
            var pcIp = "http://localhost:5002";

            // Determine the base address to use based on the platform
            var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;

            var httpClient = new HttpClient { BaseAddress = new Uri(baseAddress) };
            _productService = new ProductService(httpClient);
            Products = new ObservableCollection<Product>();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            var products = await _productService.GetProductsAsync();
            Products.Clear();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }
    }
}
