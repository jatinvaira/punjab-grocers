using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;
using PGA.Models;
using System.Linq;

namespace PGA.ViewModels
{
    public class CheckoutViewModel : BindableObject
    {
        public ObservableCollection<CartItem> CartItems { get; private set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalPrice => CartItems.Sum(item => item.TotalPrice);
        public ICommand PlaceOrderCommand { get; }

        public CheckoutViewModel(ObservableCollection<CartItem> cartItems)
        {
            CartItems = cartItems;
            PlaceOrderCommand = new Command(PlaceOrder);
        }

        private async void PlaceOrder()
        {
            // Logic to place the order goes here
            // For demo purposes, just display a success message
            await Application.Current.MainPage.DisplayAlert("Order Placed", "Your order has been placed successfully.", "OK");
        }
    }
}
