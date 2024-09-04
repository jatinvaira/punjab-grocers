using System.Threading.Tasks;
using PGA.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using Xamarin.Forms;
using System;

namespace PGA.Services
{
    public class ProductService
    {
        private readonly HttpClient _httpClient;

        public ProductService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = GetBaseAddress(); // Set the base address
        }

        private Uri GetBaseAddress()
        {
            var emulatorIp = "http://10.0.2.2:5002/";
            var pcIp = "http://localhost:5002";

            var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;

            return new Uri(baseAddress);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<Product>>("api/Products");
        }

        public async Task<Product> GetProductAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Product>($"api/Products/{id}");
        }

        public async Task AddProductAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Products", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task UpdateProductAsync(Product product)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Products/{product.Id}", product);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteProductAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/Products/{id}");
            response.EnsureSuccessStatusCode();
        }
    }
}
