using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xamarin.Forms;
using PGA.Models;
using Newtonsoft.Json;
using PGA.Views;
using System.Text;

namespace PGA.Services
{
    public class AuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = GetBaseAddress(); // Set the base address
        }

        private Uri GetBaseAddress()
        {
            // Localhost or emulator IP based on the platform
            var emulatorIp = "http://10.0.2.2:5002/";
            var pcIp = "http://localhost:5002";
            var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;
            return new Uri(baseAddress);
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordRequest request)
        {
            var emulatorIp = "http://10.0.2.2:5002/";
            var pcIp = "http://localhost:5002";
            var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(baseAddress, content);

            return response.IsSuccessStatusCode;
        }

        // Signup a new user
        public async Task<bool> SignUpAsync(UserSignUpRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/signup", request);
                if (response.IsSuccessStatusCode)
                {
                    return true; // Sign-up successful
                }
                return false; // Sign-up failed
            }
            catch (Exception ex)
            {
                // Handle or log exception
                throw new ApplicationException("Error during sign up", ex);
            }
        }

        // Login a user
        public async Task<string> LoginAsync(LoginRequest request)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/auth/login", request);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
                    return result?.Token; // Return token on successful login
                }
                return null; // Login failed
            }
            catch (Exception ex)
            {
                // Handle or log exception
                throw new ApplicationException("Error during login", ex);
            }
        }
    }

    // Models for requests and responses
    public class UserSignUpRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
