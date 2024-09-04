using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomeScreen : ContentPage
    {
        public WelcomeScreen()
        {
            InitializeComponent();
        }

        private async void OnGetStartedClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginScreen());
        }
    }
}
