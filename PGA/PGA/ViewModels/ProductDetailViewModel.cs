using PGA.Services;
using PGA;
using PGA.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

public class ProductDetailViewModel : BindableObject
{
    private readonly ProductService _productService;
    private Product _product;

    public Product Product
    {
        get => _product;
        set
        {
            _product = value;
            OnPropertyChanged();
        }
    }

    public ICommand UpdateCommand { get; }
    public ICommand DeleteCommand { get; }

    public ProductDetailViewModel(Product product)
    {
        _productService = App.ProductService;
        Product = product;
        UpdateCommand = new Command(async () => await UpdateProductAsync());
        DeleteCommand = new Command(async () => await DeleteProductAsync());
    }

    private async Task UpdateProductAsync()
    {
        await _productService.UpdateProductAsync(Product);
    }

    private async Task DeleteProductAsync()
    {
        await _productService.DeleteProductAsync(Product.Id);
    }
}
