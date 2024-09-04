using System;
using Xamarin.Forms;
using PGA.Services;
using PGA.Models;
using System.Net.Http;

namespace PGA.Views
{
    public partial class SignUpScreen : ContentPage
    {
        private readonly AuthService _authService;

        public SignUpScreen()
        {
            InitializeComponent();
            _authService = new AuthService(new HttpClient());
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            var request = new UserSignUpRequest
            {
                Name = nameEntry.Text,
                Email = emailEntry.Text,
                Password = passwordEntry.Text,
                
            };
            var ConfirmPassword = confirmPasswordEntry.Text;
            // Validate input
            if (request.Password != ConfirmPassword)
            {
                await DisplayAlert("Error", "Passwords do not match", "OK");
                return;
            }

            // Attempt to sign up
            bool isSuccess = await _authService.SignUpAsync(request);

            if (isSuccess)
            {
                await DisplayAlert("Success", "Sign up successful", "OK");
                // Navigate to login screen
                await Navigation.PushAsync(new LoginScreen()); // Adjust as necessary
            }
            else
            {
                await DisplayAlert("Error", "Sign up failed", "OK");
            }
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            // Navigate to login screen
            await Navigation.PushAsync(new LoginScreen()); // Adjust as necessary
        }

    }
}
