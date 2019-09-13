using RineClient.Services;
using RineClient.ViewModels;

namespace RineClient
{
    static public class Startup
    {
        static public void Configure()
        {
            ConfigureNavigation();
        }

        private static void ConfigureNavigation()
        {
            NavigationService.Register<MainViewModel, MainPage>();

            NavigationService.Register<AuthenticationViewModel, AuthenticationPage>();
        }
    }
}
