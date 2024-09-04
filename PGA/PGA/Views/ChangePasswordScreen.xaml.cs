using System;
using Xamarin.Forms;
using System.Net.Http;
using System.Threading.Tasks;
using PGA.Services;

namespace PGA.Views
{
    public partial class ChangePasswordScreen : ContentPage
    {
        private readonly AuthService _authService;

        public ChangePasswordScreen()
        {
            InitializeComponent();
            _authService = new AuthService(new HttpClient());
        }

        private async void OnChangePasswordClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(emailEntry.Text) ||
                string.IsNullOrWhiteSpace(currentPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(newPasswordEntry.Text) ||
                string.IsNullOrWhiteSpace(confirmNewPasswordEntry.Text))
            {
                messageLabel.Text = "Please fill in all fields.";
                messageLabel.IsVisible = true;
                return;
            }

            if (newPasswordEntry.Text != confirmNewPasswordEntry.Text)
            {
                messageLabel.Text = "New passwords do not match.";
                messageLabel.IsVisible = true;
                return;
            }

            var request = new ChangePasswordRequest
            {
                Email = emailEntry.Text,
                CurrentPassword = currentPasswordEntry.Text,
                NewPassword = newPasswordEntry.Text
            };

            // Attempt to change password
            bool isPasswordChanged = await _authService.ChangePasswordAsync(request);

            if (isPasswordChanged)
            {
                await DisplayAlert("Success", "Password changed successfully.", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                messageLabel.Text = "Failed to change password. Please check your current password and try again.";
                messageLabel.IsVisible = true;
            }
        }
    }

    public class ChangePasswordRequest
    {
        public string Email { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
