using RineClient.Services;
using RineClient.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace RineClient.ViewModels
{
    public class MainViewModel
    {
        #region constructor
        public MainViewModel(INavigationService navigationService, ChatService chatService)
        {
            _navigationService = navigationService;
            _chatService = chatService;
        }
        #endregion

        #region property
        public RineUser User { get; set; }

        public HubConnection ChatHub { get; set; }
        #endregion

        #region field
        private INavigationService _navigationService;
        private IChatService _chatService;
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
