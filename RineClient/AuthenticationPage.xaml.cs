using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using RineClient.ViewModels;
using RineClient.Common;
using RineClient.Services;

namespace RineClient
{
    public sealed partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            InitializeComponent();
            ViewModel = ServiceLocator.Current.GetService<AuthenticationViewModel>();
        }

        public AuthenticationViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            InitializeNavigation();

            if (ViewModel.IsAutoLogin)
            {
                ViewModel.Login();
            }
            
        }

        private void InitializeNavigation()
        {
            var navigationService = ServiceLocator.Current.GetService<INavigationService>();
            navigationService.Initialize(Frame);
        }
    }
}
