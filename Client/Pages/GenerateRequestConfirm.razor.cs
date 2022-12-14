using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages
{
    public partial class GenerateRequestConfirm
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public void ToRequestList()
        {
            NavigationManager.NavigateTo("/");
        }

        public void CreateRequest()
        {
            NavigationManager.NavigateTo("/createRequest");
        }
        
    }
}