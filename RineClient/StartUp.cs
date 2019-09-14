using Microsoft.Extensions.DependencyInjection;
using RineClient.Services;
using RineClient.ViewModels;
using RineClient.Common;

namespace RineClient
{
    static public class Startup
    {
        static private readonly ServiceCollection _serviceCollection = new ServiceCollection();

        static public void Configure()
        {
            ConfigureNavigation();

            ServiceLocator.Configure(_serviceCollection);
        }

        private static void ConfigureNavigation()
        {
            NavigationService.Register<MainViewModel, MainPage>();

            NavigationService.Register<AuthenticationViewModel, AuthenticationPage>();
        }
    }
}
