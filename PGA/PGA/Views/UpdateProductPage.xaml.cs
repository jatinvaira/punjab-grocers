using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using PGA.Models;
using PGA.ViewModels;
using PGA.Services;
using System.Net.Http;

namespace PGA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UpdateProductPage : ContentPage
    {
        private readonly ProductService _productService;

        public UpdateProductPage(Product product)
        {
            InitializeComponent();
            var httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5002") }; // Adjust as needed
            _productService = new ProductService(httpClient);
            BindingContext = new UpdateProductViewModel(product, _productService);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var viewModel = BindingContext as UpdateProductViewModel;
            await viewModel.UpdateProductAsync();
            await Navigation.PopAsync();
        }
    }
}
