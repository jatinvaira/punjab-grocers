using PGA.Services;
using PGA.Views;
using System;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PGA
{
    public partial class App : Application
    {
        public static ProductService ProductService { get; private set; }
        public App()
        {
            InitializeComponent();
            var httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5002")
            };

            ProductService = new ProductService(httpClient);
            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
