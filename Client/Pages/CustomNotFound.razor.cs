using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages
{
    public partial class CustomNotFound
    {
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public void NavigateToHome()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}