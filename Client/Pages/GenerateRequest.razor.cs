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
using Microsoft.JSInterop;
using Client.HttpRepository.HttpInterceptor;

namespace Reg.Client.Pages
{
    public partial class GenerateRequest : IDisposable
    {
        MudForm form; 
        MudForm formdoc;
        MudForm formId;
        bool dataSuccess;
        bool idSuccess;
        bool docSuccess;
        public EventCallback<RequestFileReadDto> OnChange { get; set; }
        public DateTime? PassportDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsJuridical { get; set; }
        public Country CyrrentCountry { get; set; }
        private RequestAbonentReadDto _request;
        [Inject]
        ISnackbar Snackbar { get; set; }
        [Inject] 
        private HttpInterceptorService Interceptor { get; set; }
        [Inject]
        IDialogService DialogService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        IRequestFileHttpRepository RequestFileHttpRepo { get; set; }
        [Inject]
        IRegRequestHttpRepository RequestRepo { get; set; }
        private List<string> fileNamesSelfi = new List<string>();
        public RequestFileReadDto RequestFileReadDtoSelfi = new RequestFileReadDto();
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IMapper Mapper { get; set; }
        [Inject]
        public IWebSocketService WebSocketService { get; set; }
         [Parameter]
        public Guid Id { get; set; }
        public string Search { get; set; } 
        public string providerRuName { get; set; } = string.Empty;
        public RequestFileFeatures requestFileFeatures = new RequestFileFeatures();
        public RequestFileReadDto RequestFileReadDto = new RequestFileReadDto();
        private RequestFileReadDto? _passportFile { get; set; }
        public RequestFileReadDto RequestFileReadDtoSnils = new RequestFileReadDto();
        private RequestFileReadDto? _snilsFile { get; set; }
        public RequestFileReadDto RequestFileReadDtoDov = new RequestFileReadDto();
        private RequestFileReadDto? _dovFile { get; set; }
        private RequestFileReadDto? _selfiFile { get; set; }
        MudFileUpload<IReadOnlyList<IBrowserFile>> selfiValidation;
        public RequestFileReadDto RequestFileReadDtoClaim = new RequestFileReadDto();
        private RequestFileReadDto? _claimFile { get; set; }
        List<RequestFileReadDto> requestFileList= new List<RequestFileReadDto>();
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full ";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();
        private List<string> fileNamesSnils = new List<string>();
        private List<string> fileNamesDov = new List<string>();
        private List<string> fileNamesClaim = new List<string>();

        protected async override Task OnInitializedAsync()
        {
            Interceptor.RegisterEvent();
            Interceptor.RegisterBeforeSendEvent();
            
            _request = await RequestRepo.GetRequestAbonent(Id);

            if (_request.PersonPassportDate != null)
            {
                PassportDate = DateTime.Parse(_request.PersonPassportDate);
            }

            if (_request.PersonBirthDate != null)
            {
                BirthDate = DateTime.Parse(_request.PersonBirthDate);
            }

            requestFileList = await RequestFileHttpRepo.GetRequestFiles(Id);
            _passportFile = requestFileList.Where(type => type.TypeId == 1).FirstOrDefault();
            if (_passportFile != null)
            {
               fileNames.Add(_passportFile.Name); 
               RequestFileReadDto.Id = _passportFile.Id;
               RequestFileReadDto.Name = _passportFile.Name;
            }
            _snilsFile = requestFileList.Where(type => type.TypeId == 2).FirstOrDefault();
            if (_snilsFile != null)
            {
               fileNamesSnils.Add(_snilsFile.Name); 
               RequestFileReadDtoSnils.Id = _snilsFile.Id;
               RequestFileReadDtoSnils.Name = _snilsFile.Name;
            }

            _dovFile = requestFileList.Where(type => type.TypeId == 4).FirstOrDefault();
            if (_dovFile != null)
            {
               fileNamesDov.Add(_dovFile.Name); 
               RequestFileReadDtoDov.Id = _dovFile.Id;
               RequestFileReadDtoDov.Name = _dovFile.Name;
            }

             _selfiFile = requestFileList.Where(type => type.TypeId == 5).FirstOrDefault();
            if (_selfiFile != null)
            {
               fileNamesSelfi.Add(_selfiFile.Name); 
               RequestFileReadDtoSelfi.Id = _selfiFile.Id;
            }

            _claimFile = requestFileList.Where(type => type.TypeId == 3).FirstOrDefault();
            if (_claimFile != null)
            {
               fileNamesClaim.Add(_claimFile.Name); 
               RequestFileReadDtoClaim.Id = _claimFile.Id;
               RequestFileReadDtoClaim.Name = _claimFile.Name;
            }
            Search = _request.Inn;
            if(_request.PersonCryptoProviderId == 11)
            {
                providerRuName = "Крипто Про CSP";
            }
            else if (_request.PersonCryptoProviderId == 9)
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

        private async Task SilentUpdate()
        {   
            
            await RequestRepo.UpdateRequestAbonent(Id, Mapper.Map<RequestAbonentUpdateDto>(_request));

           
        }

        private async Task Back()
        {   
            NavigationManager.NavigateTo($"/connection/{Search}", false);
        }

        private async Task GenRequest()
        {
            await Confirm();
      
        }

        private async Task Checked()
        {
            await formId.Validate();
            if (idSuccess)
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", "Нажимая кнопку \"Отправить\" Вы подтверждаете идентификацию личности лица указанного в заявке");
                parameters.Add("ButtonText", "Отправить");
                parameters.Add("Color", Color.Primary);

                var dialog = DialogService.Show<ConfirmDialog>("Подтверждение данных", parameters);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    _request.StepId = 3;
                    await Update();
                    NavigationManager.NavigateTo("/generateRequestConfirm");
                }
            }
            else if (!idSuccess)
            {
                Snackbar.Add("Загрузите селфи", Severity.Error);
            }
            
      
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



//Загрузка СНИЛС
      

        private async Task ClearSnils()
        {
            if (RequestFileReadDtoSnils.Id != Guid.Empty)
            {
                var fileBytes = await RequestFileHttpRepo.GetRequestFile(RequestFileReadDtoSnils.Id);
                var fileName = $"{RequestFileReadDtoSnils.Name}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
            }    
        }

       
//Загрузка Паспорта

        private async Task DownloadPassport()
        {
            if (RequestFileReadDto.Id != Guid.Empty)
            {
                var fileBytes = await RequestFileHttpRepo.GetRequestFile(RequestFileReadDto.Id);
                var fileName = $"{RequestFileReadDto.Name}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
            }
        }

//Загрузка Доверенность 
       

        private async Task DownloadDov()
        {
            if (RequestFileReadDtoDov.Id != Guid.Empty)
            {
                var fileBytes = await RequestFileHttpRepo.GetRequestFile(RequestFileReadDtoDov.Id);
                var fileName = $"{RequestFileReadDtoDov.Name}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
            }
        }

//Загрузка Заявление


        private async Task DownloadClaim()
        {
            if (RequestFileReadDtoClaim.Id != Guid.Empty)
            {
                var fileBytes = await RequestFileHttpRepo.GetRequestFile(RequestFileReadDtoClaim.Id);
                var fileName = $"{RequestFileReadDtoClaim.Name}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
            }


        }


        private async Task Confirm()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Нажмите кнопку \"Выполнить\", чтобы запустить криптографические операции и подготовить заявление к отправке. Предварительно скачайте и установите 1C Toolbox. ");
            parameters.Add("ButtonText", "Выполнить");
            parameters.Add("Color", Color.Primary);

            var dialog = DialogService.Show<ConfirmDialogToolbox>("Подготовка к формированию контейнера", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await RunWebSocket();
            }
        }


        public async Task RunWebSocket()
        {
            var mappedPerson = Mapper.Map<RequestAbonent>(_request);
            var messageResponse = await WebSocketService.GenerateRequest(mappedPerson);
            if (messageResponse != null)
                if(messageResponse.Success)
                    Console.WriteLine("Запрос сформирован");
                    _request.CertRequest = messageResponse.Data.value;
                    _request.StepId = 4;
                    await SilentUpdate();
                    NavigationManager.NavigateTo("/generateRequestConfirm");
        }


        //Загрузка Селфи
        private async Task OnInputFileChangedSelfi(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 5;
            await ClearSelfi();
            ClearDragClassSelfi();
            var file = e.File;
            fileNamesSelfi.Add(file.Name);
            await selfiValidation.Validate();
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDtoSelfi = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDtoSelfi);  
            }
            
        }

        private async Task ClearSelfi()
        {
            if (RequestFileReadDtoSelfi.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDtoSelfi.Id);
                RequestFileReadDtoSelfi.Id = Guid.Empty;
            }
            
            fileNamesSelfi.Clear();
            ClearDragClassSelfi();

        }

        private void SetDragClassSelfi()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClassSelfi()
        {
            DragClass = DefaultDragClass;
        }

        private string ValidationSelfi(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNamesSelfi.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
        }

        public void Dispose()
        {
            Interceptor.DisposeEvent();
        }


    }
}