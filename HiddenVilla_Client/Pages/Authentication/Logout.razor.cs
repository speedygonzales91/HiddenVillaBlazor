using HiddenVilla_Client.Service.IService;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HiddenVilla_Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService autheticationService { get; set; }
        [Inject]
        public NavigationManager navigationManager { get; set; }

        protected async override Task OnInitializedAsync()
        {
            await autheticationService.Logout();
            navigationManager.NavigateTo("/");
        }
    }
}
