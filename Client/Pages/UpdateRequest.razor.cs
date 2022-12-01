using AutoMapper;
using Client.Service;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Client.HttpRepository;

namespace Reg.Client.Pages
{
    public partial class UpdateRequest 
    {
        MudDatePicker _pickerStart, _pickerEnd;
        public bool IsJuridical { get; set; }
        private RequestAbonentReadDto _request;
        [Inject]
        ISnackbar Snackbar { get; set; }
        [Inject]
        IRegRequestHttpRepository RequestRepo { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IMapper Mapper { get; set; }
        [Inject]
        public IWebSocketService WebSocketService { get; set; }
        [Parameter]
        public Guid Id { get; set; }
        public string Search { get; set; } 
        protected async override Task OnInitializedAsync()
        {
            _request = await RequestRepo.GetRequestAbonent(Id);
            Search = _request.Inn;
        }
        

        
        private async Task Update()
        {   
            
            await RequestRepo.UpdateRequestAbonent(Id, Mapper.Map<RequestAbonentUpdateDto>(_request));
            Snackbar.Add("Заявление успешно сохранено!", Severity.Success);
           
        }

        private async Task Back()
        {   
            NavigationManager.NavigateTo($"/connection/{Search}", false);
        }

        private async Task GenerateRequest()
        {
            var mapReqAbon = Mapper.Map<RequestAbonent>(_request);
            
            await WebSocketService.GenerateRequest(mapReqAbon);
            Snackbar.Add("Запрос успешно сформирован!", Severity.Success);
           
        }
        
    }
}