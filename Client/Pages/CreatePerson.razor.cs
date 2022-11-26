using Microsoft.AspNetCore.Components;
using MudBlazor;
using Entities.Models;
using Reg.Client.HttpRepository;

namespace Reg.Client.Pages
{
    public partial class CreatePerson 
    {
        private Person _person = new Person();

        [Inject]
        public IPersonHttpRepository PersonRepo { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        DateTime? date = DateTime.Today;

        private async Task Create()
        {
            await PersonRepo.CreatePerson(_person);
            // Snackbar.Add("Аванс создан!", Severity.Success);
            // NavigationManager.NavigateTo("/prepayments");
        }


    }
}