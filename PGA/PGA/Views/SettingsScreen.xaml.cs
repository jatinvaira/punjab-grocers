using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsScreen : ContentPage
    {
        public SettingsScreen()
        {
            InitializeComponent();
        }

        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChangePasswordScreen());
        }

        private void OnLogoutClicked(object sender, EventArgs e)
        {
            // Handle logout logic here

            // Navigate back to the login screen
            Application.Current.MainPage = new NavigationPage(new LoginScreen());
        }
    }
}
