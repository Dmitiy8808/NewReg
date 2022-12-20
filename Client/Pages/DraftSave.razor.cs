using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages
{
    public partial class DraftSave
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