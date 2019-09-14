using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.SignalR.Client;
using System.Windows.Input;
using RineClient.Common;
using RineSignalRContracts.ControllerModels;
using RineClient.Models;
using RineClient.Services;

namespace RineClient.ViewModels
{
    public class AuthenticationViewModel : INotifyPropertyChanged
    {
        #region constructor
        public AuthenticationViewModel(INavigationService navigationService)
        {
            _client.BaseAddress = new Uri(RineSettings.BaseUri);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            _credentials = GetCredentialsFromLocker();

            NavigationService = navigationService;

            LoadSettings();
        }
        #endregion

        #region property
        private string _userName;
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public IEnumerable<string> SavedUsers { get => _credentials.Select(c => c.UserName); }

        public bool IsAutoLogin { get; set; }

        public bool IsRememberPswd { get; set; }

        public HubConnection ChatHub { get; set; }

        public RineUser User { get; set; }

        public IEnumerable<string> Errors { get; set; }

        public INavigationService NavigationService { get; }
        #endregion

        #region field
        private IReadOnlyList<Windows.Security.Credentials.PasswordCredential> _credentials;
        private readonly HttpClient _client = new HttpClient();
        #endregion

        #region helper
        private async Task<RineUser> DoLoginAsync()
        {
            var token = await GetTokenAsync();
            if (token != null)
            {
                ChatHub = new HubConnectionBuilder()
                    .WithUrl(RineSettings.ChatHubUri, options =>
                    {
                    // Consider using GetTokenAsync()?
                    options.AccessTokenProvider = () => Task.FromResult(token);
                    })
                    .Build();

                // auto reconnect
                ChatHub.Closed += async (error) =>
                {
                    await Task.Delay(new Random().Next(0, 5) * 1000);
                    await ChatHub.StartAsync();
                };

                return new RineUser
                {
                    UserName = UserName,
                };
            }
            else
            {
                return null;
            }
        }

        private async Task<string> GetTokenAsync()
        {
            var response = await _client.PostAsJsonAsync(RineSettings.TokenUri, new UserLogin
            {
                UserName = UserName,
                Password = Password,
            });

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<TokenResult>();
            if (result.Code == 0)
            {
                return result.Token;
            }
            else
            {
                Errors = result.Messages;
                return null;
            }
        }

        private static IReadOnlyList<Windows.Security.Credentials.PasswordCredential> GetCredentialsFromLocker()
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            try
            {
                return vault.FindAllByResource("Rine");
            }
            catch
            {
                return null;
            }
        }

        private static string GetCredentialPasswordFromLocker(string userName)
        {
            var vault = new Windows.Security.Credentials.PasswordVault();
            var credential = vault.Retrieve("Rine", userName);
            credential.RetrievePassword();
            return credential.Password;
        }

        private static void SaveCredentialsToLocker(string userName, string password)
            => (new Windows.Security.Credentials.PasswordVault()).Add(new Windows.Security.Credentials.PasswordCredential(
                "Rine", userName, password));

        private void SaveSettings()
        {
            RineSettings settings = new RineSettings();
            settings["IsRememberPswd"] = IsRememberPswd;
            settings["IsAutoLogin"] = IsAutoLogin;
        }

        private void LoadSettings()
        {
            RineSettings settings = new RineSettings();
            IsRememberPswd = (bool)settings.SetDefault("IsRememberPswd", false);
            IsAutoLogin = (bool)settings.SetDefault("IsRememberPswd", false);
        }
        #endregion

        #region method
        public async void Login()
        {
            User = await DoLoginAsync();

            if (User != null && IsRememberPswd)
            {
                SaveCredentialsToLocker(UserName, Password);
                SaveSettings();

                NavigationService.Navigate<MainViewModel>(new MainArgs
                {
                    User = User,
                    ChatHub = ChatHub,
                });
            }
        }

        public void NameSelectionChanged(string selectedItem)
        {
            Password = GetCredentialPasswordFromLocker(selectedItem);
        }
        #endregion

        #region command
        public ICommand LoginCommand => new RelayCommand(Login);

        public ICommand NameSelectionChangedCommand => new RelayCommand<string>(NameSelectionChanged);

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion
    }
}
