using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using RineClient.ViewModels;
using RineClient.Services;

namespace RineClient
{
    public sealed partial class AuthenticationPage : Page
    {
        public AuthenticationPage()
        {
            InitializeComponent();
            ViewModel = new AuthenticationViewModel(new NavigationService(Frame));
        }

        public AuthenticationViewModel ViewModel { get; set; }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (ViewModel.IsAutoLogin)
            {
                ViewModel.Login();
            }
            
        }
    }
}
