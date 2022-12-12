using AutoMapper;
using Client.Service;
using Entities.DTOs;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using Client.HttpRepository;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using Entities.FileFeatures;

namespace Reg.Client.Pages
{
    public partial class UpdateRequest 
    {
        public RequestFileReadDto RequestFileReadDto = new RequestFileReadDto();
        public EventCallback<RequestFileReadDto> OnChange { get; set; }
        public DateTime? PassportDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public string SelectedOptionGender { get; set; }
        MudDatePicker _pickerStart, _pickerEnd;
        public bool IsJuridical { get; set; }
        public Country CyrrentCountry { get; set; }
        private RequestAbonentReadDto _request;
        [Inject]
        ISnackbar Snackbar { get; set; }
        [Inject]
        IRequestFileHttpRepository RequestFileHttpRepo { get; set; }
        [Inject]
        IRegRequestHttpRepository RequestRepo { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IMapper Mapper { get; set; }
        [Inject]
        public IWebSocketService WebSocketService { get; set; }
        [Inject]
        public ICountryHttpRepository CountryHttpRepository { get; set; }
        [Parameter]
        public Guid Id { get; set; }
        public string Search { get; set; } 
        public string providerRuName { get; set; } = string.Empty;
        public RequestFileFeatures requestFileFeatures = new RequestFileFeatures();
        private RequestFileReadDto? _passportFile { get; set; }
        List<RequestFileReadDto> requestFileList= new List<RequestFileReadDto>();
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full ";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();
        protected async override Task OnInitializedAsync()
        {
            _request = await RequestRepo.GetRequestAbonent(Id);
            requestFileList = await RequestFileHttpRepo.GetRequestFiles(Id);
            _passportFile = requestFileList.Where(type => type.TypeId == 1).FirstOrDefault();
            if (_passportFile != null)
            {
               fileNames.Add(_passportFile.Name); 
               RequestFileReadDto.Id = _passportFile.Id;
            }
            Search = _request.Inn;
            if(_request.PersonCryptoProviderId == 11)
            {
                providerRuName = "Крипто Про CSP";
            }
            else if (_request.PersonCryptoProviderId == 11)
            {
                providerRuName = "VipNet CSP";
            }

            CyrrentCountry = new Country 
            {
                Code="643",
                CountryName = "Россия"
            };
            _request.PersonCountry = "643";
            _request.PersonPassportType = 1;
            requestFileFeatures.RequestAbonentId = _request.Id;
        }
        
        void CountryChanged(Country value)
        {
            CyrrentCountry = value;
            _request.PersonCountry = value.Code;
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

        private async Task<IEnumerable<Country>> SearchCountry(string value)
        {

            var countries = await CountryHttpRepository.GetCountries();

            if (string.IsNullOrEmpty(value))
                return countries;
            return countries.Where(x => x.CountryName.Contains(value, StringComparison.InvariantCultureIgnoreCase));
        }


        void PassportDateChanged(DateTime? date)
        {
            PassportDate = date;
            _request.PersonPassportDate = date.HasValue ? date.Value.ToString("dd.MM.yyyy") : "";

        }

        void BirthDateChanged(DateTime? date)
        {
            BirthDate = date;
            _request.PersonBirthDate = date.HasValue ? date.Value.ToString("dd.MM.yyyy") : "";

        }

        void GenderChanged(string selectedOption)
        {
            if(selectedOption == "1")
            {
                _request.PersonGender = 1;
            }
            else if (selectedOption == "2")
            {
                _request.PersonGender = 2;
            }
        }

//Загрузка СНИЛС
        private async Task OnInputFileChangedSnils(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 1;
            await ClearSnils();
            ClearDragClassSnils();
            var file = e.File;
            fileNames.Add(file.Name);
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDto = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDto);  
            }
            
        }

        private async Task ClearSnils()
        {
            if (RequestFileReadDto.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDto.Id);
                RequestFileReadDto.Id = Guid.Empty;
            }
            
            fileNames.Clear();
            ClearDragClassSnils();

        }

        private void SetDragClassSnils()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClassSnils()
        {
            DragClass = DefaultDragClass;
        }

        //Загрузка паспорта
        private async Task OnInputFileChanged(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 1;
            await Clear();
            ClearDragClass();
            var file = e.File;
            fileNames.Add(file.Name);
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDto = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDto);  
            }
            
        }

        private async Task Clear()
        {
            if (RequestFileReadDto.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDto.Id);
                RequestFileReadDto.Id = Guid.Empty;
            }
            
            fileNames.Clear();
            ClearDragClass();

        }

        private void SetDragClass()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClass()
        {
            DragClass = DefaultDragClass;
        }
        
    }
}