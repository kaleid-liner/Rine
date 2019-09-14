using RineClient.Services;
using RineClient.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace RineClient.ViewModels
{
    public class MainViewModel
    {
        #region constructor
        public MainViewModel(INavigationService navigationService)
        {
            NavigationService = navigationService;
        }
        #endregion

        #region property
        public INavigationService NavigationService { get; }

        public RineUser User { get; set; }

        public HubConnection ChatHub { get; set; }
        #endregion

        #region method
        public Task LoadAsync(MainArgs args)
        {
            User = args.User;
            ChatHub = args.ChatHub;

            return Task.CompletedTask;
        }
        #endregion
    }
}
