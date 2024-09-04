using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using PGA.Models;

namespace PGA.ViewModels
{
    public class CartViewModel : BindableObject
    {
        public ObservableCollection<CartItem> CartItems { get; private set; }
        public ICommand RemoveItemCommand { get; }

        public CartViewModel()
        {
            CartItems = new ObservableCollection<CartItem>();
            RemoveItemCommand = new Command<CartItem>(RemoveItem);
        }

        public void AddItem(Product product)
        {
            var existingItem = CartItems.FirstOrDefault(i => i.ProductId == product.Id);
            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                CartItems.Add(new CartItem
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    Quantity = 1,
                    Price = product.Price
                });
            }

            OnPropertyChanged(nameof(TotalPrice));
        }

        public void RemoveItem(CartItem item)
        {
            CartItems.Remove(item);
            OnPropertyChanged(nameof(TotalPrice));
        }

        public decimal TotalPrice => CartItems.Sum(item => item.TotalPrice);
    }
}
