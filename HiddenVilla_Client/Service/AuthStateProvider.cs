using Blazored.LocalStorage;
using Common;
using HiddenVilla_Client.Helper;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Service
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient _client;
        private readonly ILocalStorageService _localStorage;

        public AuthStateProvider(HttpClient client, ILocalStorageService localStorage)
        {
            this._client = client;
            this._localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>(SD.Local_Token);
            if (token is null)
            {
                return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity()));
            }
            _client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
        }

        public void NotifyUserLoggedIn(string token)
        {
            var authenticationUser = new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token) , "jwtAuthType"));
            var authState = Task.FromResult(new AuthenticationState(authenticationUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLoggedOut()
        {
            var authState = Task.FromResult(new AuthenticationState(new System.Security.Claims.ClaimsPrincipal(new ClaimsIdentity())));
            NotifyAuthenticationStateChanged(authState);
        }

    }
}
