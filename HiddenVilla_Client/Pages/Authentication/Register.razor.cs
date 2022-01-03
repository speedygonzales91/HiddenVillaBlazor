﻿using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Register
    {
        private UserRequestDTO userForRegistration = new UserRequestDTO();

        public bool IsProcessing { get; set; }

        public bool ShowRegistrationError { get; set; }

        public IEnumerable<string> Errors { get; set; }
        [Inject]
        public IAuthenticationService autheticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        private async Task RegisterUser()
        {
            ShowRegistrationError = false;
            IsProcessing = true;

            var result = await autheticationService.RegisterUser(userForRegistration);
            if (result.IsRegistrationSuccessful)
            {
                IsProcessing = false;
                navigationManager.NavigateTo("/login");
            }
            else
            {
                IsProcessing = false;
                Errors = result.Errors;
                ShowRegistrationError = true;
            }
        }
    }
}
