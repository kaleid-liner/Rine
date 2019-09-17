using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using RineClient.Common;
using RineClient.Models;
using RineSignalRContracts.ControllerModels;

namespace RineClient.Services
{
    public class ChatService : IChatService
    {
        public HubConnection ChatHub { get; private set; }

        public async Task<LoginResult> LoginAsync(string userName, string password)
        {
            var tokenResult = await GetTokenAsync(userName, password);
            var token = tokenResult.Token;

            if (tokenResult.ResultType == TokenResult.TokenResultType.Success)
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

                return new LoginResult
                {
                    ResultType = LoginResult.LoginResultType.Success,
                };
            }
            else
            {
                return new LoginResult
                {
                    ResultType = LoginResult.LoginResultType.Failed,
                    Errors = tokenResult.Messages,
                };
            }
        }

        private async Task<TokenResult> GetTokenAsync(string userName, string password)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(RineSettings.BaseUri)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync(RineSettings.TokenUri, new UserLogin
            {
                UserName = userName,
                Password = password,
            });

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TokenResult>();
        }

        public async Task LogoutAsync()
        {
            await ChatHub.StopAsync();
        }
    }
}
