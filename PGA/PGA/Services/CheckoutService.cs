using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System;
using PGA.Models;
using Xamarin.Forms;

public class CheckoutService
{
    private readonly HttpClient _client;

    public CheckoutService()
    {
        _client = new HttpClient();
    }

    public async Task<List<Checkout>> GetCheckoutsAsync()
    {
        var emulatorIp = "http://10.0.2.2:5002/api/checkouts";
        var pcIp = "http://localhost:5002/api/checkouts";
        var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;

        try
        {
            var response = await _client.GetAsync(baseAddress);
            System.Diagnostics.Debug.WriteLine($"API Response Status Code: {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"API Response Body: {responseBody}");
                return JsonConvert.DeserializeObject<List<Checkout>>(responseBody);
            }
            else
            {
                var errorMessage = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Error: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
        }

        return new List<Checkout>();
    }


    public async Task DeleteCheckoutAsync(int id)
    {
        // Choose the appropriate URL based on the platform
        var emulatorIp = $"http://10.0.2.2:5002/api/checkouts/{id}";
        var pcIp = $"http://localhost:5002/api/checkouts/{id}";
        var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;

        try
        {
            var response = await _client.DeleteAsync(baseAddress);

            if (!response.IsSuccessStatusCode)
            {
                // Log error or display message based on status code
                var errorMessage = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Error: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
        }
    }

    public async Task<Checkout> CreateCheckoutAsync(Checkout checkout)
    {
        var json = JsonConvert.SerializeObject(checkout);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        // Choose the appropriate URL based on the platform
        var emulatorIp = "http://10.0.2.2:5002/api/checkout";
        var pcIp = "http://localhost:5002/api/checkout";
        var baseAddress = Device.RuntimePlatform == Device.Android ? emulatorIp : pcIp;

        try
        {
            var response = await _client.PostAsync(baseAddress, content);

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Checkout>(responseBody);
            }
            else
            {
                // Log error or display message based on status code
                var errorMessage = await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine($"Error: {errorMessage}");
            }
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}");
        }

        return null;
    }
}
