using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PGA.Views
{
    public partial class SuccessPage : ContentPage
    {
        public SuccessPage()
        {
            InitializeComponent();
            DelayAndNavigateBack();
        }

        private async void DelayAndNavigateBack()
        {
            // Wait for 5 seconds
            await Task.Delay(5000);

            // Navigate back to the Home Screen
            await Navigation.PopToRootAsync();
        }
    }
}
