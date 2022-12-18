using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.HttpRepository;
using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages.Authentication
{
    public partial class Logout
    {
        [Inject]
        public IAuthenticationService AuthenticationService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await AuthenticationService.Logout();
            NavigationManager.NavigateTo("/");
        }
    }
}