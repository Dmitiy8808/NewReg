using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace Reg.Client.Shared
{
    public partial class MainLayout
    {
        [CascadingParameter]
        public Task<AuthenticationState> AuthenticationState {get; set; }
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        NavigationManager NavigationManager { get; set; }
        private bool _rightToLeft = false;
        bool _drawerOpen = true;

         protected async override Task OnParametersSetAsync()
        {
            var state = await AuthenticationState;
            
            if(!state.User.Identity.IsAuthenticated)
            {
                NavigationManager.NavigateTo("login");
            }
        }

        void DrawerToggle()
        {
            _drawerOpen = !_drawerOpen;
        }

        MudTheme MyCustomTheme = new MudTheme()
        {
            Palette = new Palette()
            {
                Primary = Colors.Yellow.Darken1,
                PrimaryContrastText = Colors.Shades.Black,
                Secondary = Colors.Green.Accent4,
                AppbarBackground = Colors.Yellow.Darken1,
                Background = Colors.Grey.Lighten5
            }

        };
        
        private void Logout()
        {
            var parameters = new DialogParameters
            {
                {nameof(Dialogs.Logout.ContentText), "Подтверждение выхода"},
                {nameof(Dialogs.Logout.ButtonText), "Выход"},
                {nameof(Dialogs.Logout.Color), Color.Error},

            };

            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true };

             DialogService.Show<Dialogs.Logout>("Выход", parameters, options);
        }
    }
}