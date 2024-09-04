using Xamarin.Forms;
using PGA.Models;

namespace PGA.Views
{
    public partial class ProductDetailPage : ContentPage
    {
        public ProductDetailPage(Product product)
        {
            InitializeComponent();
            BindingContext = product;
        }
    }
}
