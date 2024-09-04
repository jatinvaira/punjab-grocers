using System.Threading.Tasks;
using PGA.Models;
using PGA.Services;

namespace PGA.ViewModels
{
    public class UpdateProductViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        public Product Product { get; set; }

        public UpdateProductViewModel(Product product, ProductService productService)
        {
            Product = product;
            _productService = productService;
        }

        public async Task UpdateProductAsync()
        {
            await _productService.UpdateProductAsync(Product);
        }
    }
}
