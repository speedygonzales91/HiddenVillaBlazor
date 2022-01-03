using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components.Authorization;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;
        private readonly AuthenticationStateProvider _authStateProvider;

        public AuthenticationService(HttpClient client, ILocalStorageService localStorage, AuthenticationStateProvider  authStateProvider)
        {
            this._client = client;
            this._localStorage = localStorage;
            this._authStateProvider = authStateProvider;
        }

        public async Task<AuthenticationResponseDTO> Login(AuthenticationDTO userFromAuthentication)
        {
            var content = JsonConvert.SerializeObject(userFromAuthentication);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/account/signin", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<AuthenticationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {

                await _localStorage.SetItemAsync(SD.Local_Token, res.Token);
                await _localStorage.SetItemAsync(SD.Local_UserDetails, res.UserDTO);

                ((AuthStateProvider)_authStateProvider).NotifyUserLoggedIn(res.Token);

                _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", res.Token);

                return new AuthenticationResponseDTO { IsAuthSuccessful = true };
            }
            else
            {
                return res;
            }
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(SD.Local_Token);
            await _localStorage.RemoveItemAsync(SD.Local_UserDetails);
            _client.DefaultRequestHeaders.Authorization = null;
            ((AuthStateProvider)_authStateProvider).NotifyUserLoggedOut();

        }

        public async Task<RegistrationResponseDTO> RegisterUser(UserRequestDTO user)
        {
            var content = JsonConvert.SerializeObject(user);

            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/account/signup", bodyContent);

            var contentTemp = await response.Content.ReadAsStringAsync();

            var res = JsonConvert.DeserializeObject<RegistrationResponseDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return new RegistrationResponseDTO { IsRegistrationSuccessful = true };
            }
            else
            {
                return res;
            }
        }
    }
}
