using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Threading.Tasks;

namespace PGA.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashScreen : ContentPage
    {
        public SplashScreen()
        {
            InitializeComponent();
            AnimateLogo();
        }

        private async void AnimateLogo()
        {
            // Initial delay
            await Task.Delay(500);

            // Logo bounce animation
            await LogoImage.ScaleTo(1.2, 1000, Easing.BounceOut);
            await LogoImage.ScaleTo(1, 500, Easing.BounceIn);

            // Fade in welcome text
            await WelcomeLabel.FadeTo(1, 2000);

            // Navigate to the WelcomeScreen
            await Task.Delay(2000);
            Application.Current.MainPage = new NavigationPage(new WelcomeScreen());
        }
    }
}
