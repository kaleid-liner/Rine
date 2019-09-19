using RineClient.Services;
using RineClient.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using RineClient.Common;
using RineClient.Controls;

namespace RineClient.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        #region constructor
        public MainViewModel(INavigationService navigationService, IChatService chatService, FriendListViewModel friendList)
        {
            _navigationService = navigationService;
            _chatService = chatService;
            FriendList = friendList;
        }
        #endregion

        #region property
        public RineUser User { get; set; }

        public bool IsMainSplitViewOpen
        {
            get => _isMainSplitViewOpen;
            set
            {
                _isMainSplitViewOpen = value;
                OnPropertyChanged(nameof(IsMainSplitViewOpen));
            }
        }

        public FriendListViewModel FriendList { get; set; }
        #endregion

        #region field
        private INavigationService _navigationService;
        private IChatService _chatService;
        private bool _isMainSplitViewOpen;
        #endregion

        #region method
        public Task LoadAsync(MainArgs args)
        {
            User = args.User;
            FriendList.LoadAsync(args);

            return Task.CompletedTask;
        }

        public void OnPaneToggleButtonClick()
        {
            IsMainSplitViewOpen = !IsMainSplitViewOpen;
        }

        #endregion

        #region command
        public ICommand AccountItemClickCommand => new RelayCommand<ItemClickEventArgs>(OnAccountItemClick);
        #endregion

        #region helper
        private void OnAccountItemClick(ItemClickEventArgs args)
        {
            var item = args.ClickedItem as NavLink;

            switch (item.Label)
            {
                case "Switch Account":
                    OnSwitchAccount();
                    break;
                case "New Friend":
                    OnNewFriend();
                    break;
                case "Settings":
                    OnOpenSettings();
                    break;
            }

        }

        private void OnSwitchAccount()
        {
            _chatService.LogoutAsync();

            _navigationService.Navigate<AuthenticationViewModel>();
        }

        private void OnNewFriend()
        {

        }

        private void OnOpenSettings()
        {

        }
        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
