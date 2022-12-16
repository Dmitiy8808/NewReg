using Microsoft.AspNetCore.Components;

namespace Reg.Client.Pages
{
    public partial class CreateRequestConfirm
    {
        [Inject]
        NavigationManager NavigationManager { get; set; }

        public void ToRequestList()
        {
            NavigationManager.NavigateTo("/requestsDraft");
        }

        public void CreateRequest()
        {
            NavigationManager.NavigateTo("/createRequest");
        }
        
    }
}