using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using PGA.Services;
using PGA.Models;
using System.Net.Http;

namespace PGA.Views
{
    public partial class LoginScreen : ContentPage
    {
        private readonly AuthService _authService;

        public LoginScreen()
        {
            InitializeComponent();
            _authService = new AuthService(new HttpClient());
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var email = emailEntry.Text;
            var password = passwordEntry.Text;

            var request = new LoginRequest
            {
                Email = email,
                Password = password
            };

            var token = await LoginAsync(request);

            if (token != null)
            {
                await DisplayAlert("Success", "Login successful", "OK");
                await Navigation.PushAsync(new HomeScreen());
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }

        private async Task<string> LoginAsync(LoginRequest request)
        {
            try
            {
                var token = await _authService.LoginAsync(request);
                return token;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", "An error occurred while logging in" + ex, "OK");
                return null;
            }
        }

        private async void OnForgotPasswordClicked(object sender, EventArgs e)
        {
            // Handle forgot password logic
            await Navigation.PushAsync(new ChangePasswordScreen());
        }

        private async void OnSignUpClicked(object sender, EventArgs e)
        {
            // Navigate to sign-up screen
            await Navigation.PushAsync(new SignUpScreen());
        }
    }
}
