using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Login
    {
        private AuthenticationDTO userForAuthentication = new AuthenticationDTO();

        public bool IsProcessing { get; set; }

        public bool ShowAuthenticationError { get; set; }

        public string ReturnUrl { get; set; }

        public string Error { get; set; }
        [Inject]
        public IAuthenticationService autheticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task LoginUser()
        {
            ShowAuthenticationError = false;
            IsProcessing = true;

            var result = await autheticationService.Login(userForAuthentication);
            if (result.IsAuthSuccessful)
            {
                IsProcessing = false;

                var absoluteUrl = new Uri(navigationManager.Uri);
                var queryParam = HttpUtility.ParseQueryString(absoluteUrl.Query);
                ReturnUrl = queryParam["returnUrl"];
                if (string.IsNullOrEmpty(ReturnUrl))
                {
                    navigationManager.NavigateTo("/");
                }
                else
                {
                    navigationManager.NavigateTo("/" + ReturnUrl);
                }        
            }
            else
            {
                IsProcessing = false;
                Error = result.ErrorMessage;
                ShowAuthenticationError = true;
            }
        }
    }
}

