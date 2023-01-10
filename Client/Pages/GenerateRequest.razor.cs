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
        MudForm formCertBlanck;
        bool certBlanckSuccess;
        MudForm formId;
        MudForm formCert;
        bool dataSuccess;
        bool idSuccess;
        bool certSuccess;
        bool docSuccess;
        public EventCallback<RequestFileReadDto> OnChange { get; set; }
        MudFileUpload<IReadOnlyList<IBrowserFile>> certBlanckValidation;
        public RequestFileReadDto RequestFileReadDtoCertBlanck = new RequestFileReadDto();
        private List<string> fileNamesCertBlanck = new List<string>();
        public DateTime? PassportDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsJuridical { get; set; }
        public Country CyrrentCountry { get; set; }
        private RequestAbonentReadDto _request;
        [Inject]
        ISnackbar Snackbar { get; set; }
        [Inject]
        IPdfGeneratorHttpRepository PdfGeneratorHttpRepo { get; set; }
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
        private List<string> fileNamesCert = new List<string>();
        private List<string> fileNamesSecurityGuide = new List<string>();
        public RequestFileReadDto RequestFileReadDtoSelfi = new RequestFileReadDto();
        public RequestFileReadDto RequestFileReadDtoCert = new RequestFileReadDto();
        public RequestFileReadDto RequestFileReadDtoSecurityGuide = new RequestFileReadDto();
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
        private RequestFileReadDto? _certFile { get; set; } 
        private RequestFileReadDto? _certBlanckFile { get; set; }
        MudFileUpload<IReadOnlyList<IBrowserFile>> selfiValidation;
        MudFileUpload<IReadOnlyList<IBrowserFile>> certValidation;
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
            RequestFileReadDtoSecurityGuide.Name = "Руководство";
            fileNamesSecurityGuide.Add(RequestFileReadDtoSecurityGuide.Name);

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

             _certFile = requestFileList.Where(type => type.TypeId == 6).FirstOrDefault();
            if (_certFile != null)
            {
               fileNamesCert.Add(_request.ContainerName.Substring(0, _request.ContainerName.Length - 36)); 
               RequestFileReadDtoCert.Id = _certFile.Id;
               RequestFileReadDtoCert.Name = _request.ContainerName;

            }

             _certBlanckFile = requestFileList.Where(type => type.TypeId == 7).FirstOrDefault();
            if (_certBlanckFile != null)
            {
               fileNamesCertBlanck.Add(_certBlanckFile.Name); 
               RequestFileReadDtoCertBlanck.Id = _certBlanckFile.Id;
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

        private async Task InstallCert()
        {
            
            var checkPlugin = await WebSocketService.CheckPlugin();
            if(checkPlugin)
            {
                var certData = await RequestRepo.GetCertificateData(_request.Id);
                var messageResponse = await WebSocketService.InstallCertificate(certData);
                if (messageResponse != null)
                if(messageResponse.Success)
                {
                    Console.WriteLine("Сертификат установлен");
                    _request.StepId = 8;
                    await SilentUpdate();
                    Snackbar.Add("Сертификат установлен", Severity.Success);
                    NavigationManager.NavigateTo("/");
                }
                else
                {
                    var parameters = new DialogParameters();
                    parameters.Add("ContentText", messageResponse.Message);
                    parameters.Add("ButtonText", "OK");
                    parameters.Add("Color", Color.Error);

                    var dialog = DialogService.Show<ErrorDialog>("Ошибка установки сертификата", parameters);
                    var result = await dialog.Result;
                }
            }
            else
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", "Для продолжения работы запустите или установите плагин \"1Сtoolbox\"");
                parameters.Add("ButtonText", "OK");
                parameters.Add("Color", Color.Error);

                var dialog = DialogService.Show<ConfirmDialogToolboxInstall>("Ошибка", parameters);
                var result = await dialog.Result;
            }
            
                    
        }

        private async Task CheckCert()
        {
        
            _request.StepId = 7;
            await SilentUpdate();
            Snackbar.Add("Бланк проверен", Severity.Success);
            NavigationManager.NavigateTo("/");
        }

        private async Task SendSert()
        {
            await certBlanckValidation.Validate();
            await formCertBlanck.Validate();
            if (certBlanckSuccess)
            {
                    Console.WriteLine("Сертификат установлен");
                    _request.StepId = 6;
                    await SilentUpdate();
                    Snackbar.Add("Бланк отправлен", Severity.Success);
                    NavigationManager.NavigateTo("/");
            }
            else
            {
                Snackbar.Add("Загрузите бланк сертификата", Severity.Warning);
            }
        }

        private async Task LoadCert()
        {
            await formCert.Validate();
            if (certSuccess)
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", "Нажимая кнопку отправить вы подтверждаете выпуск сертификата");
                parameters.Add("ButtonText", "Отправить");
                parameters.Add("Color", Color.Primary);

                var dialog = DialogService.Show<ConfirmDialog>("Подтверждение выпуска сертификата", parameters);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    _request.StepId = 5;
                    await SilentUpdate();
                    NavigationManager.NavigateTo("/");
                }
            }
            else if (!certSuccess)
            {
                Snackbar.Add("Загрузите сертификат", Severity.Error);
            }
      
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
                    await SilentUpdate();
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
      

        private async Task DownloadSnils()
        {
            if (RequestFileReadDtoSnils.Id != Guid.Empty)
            {
                var fileBytes = await RequestFileHttpRepo.GetRequestFile(RequestFileReadDtoSnils.Id);
                var fileName = $"{RequestFileReadDtoSnils.Name}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
            }    
        }

        private async Task DownloadCertificate()
        {
            if (RequestFileReadDtoCert.Id != Guid.Empty)
            {
                var fileBytes = await RequestFileHttpRepo.GetRequestFile(RequestFileReadDtoCert.Id);
                var fileName = $"{RequestFileReadDtoCert.Name}.cer";
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
            var checkPlugin = await WebSocketService.CheckPlugin();
            if(checkPlugin)
            {
                var parameters = new DialogParameters();
                parameters.Add("ContentText", "Нажмите кнопку \"Выполнить\", чтобы запустить криптографические операции и подготовить заявление к отправке.");
                parameters.Add("ButtonText", "Выполнить");
                parameters.Add("Color", Color.Primary);

                var dialog = DialogService.Show<ConfirmDialogToolbox>("Подготовка к формированию контейнера", parameters);
                var result = await dialog.Result;
                if (!result.Cancelled)
                {
                    await RunWebSocket();
                }
            }
            else
                {
                    var parameters = new DialogParameters();
                    parameters.Add("ContentText", "Для продолжения работы запустите или установите плагин \"1Сtoolbox\"");
                    parameters.Add("ButtonText", "OK");
                    parameters.Add("Color", Color.Error);

                    var dialog = DialogService.Show<ConfirmDialogToolboxInstall>("Ошибка", parameters);
                    var result = await dialog.Result;
                }

        }


        public async Task RunWebSocket()
        {
            var mappedPerson = Mapper.Map<RequestAbonent>(_request);
            var certRequestData = await WebSocketService.GetCertRequestData(mappedPerson);
            var messageResponse = await WebSocketService.GenerateRequest(mappedPerson, certRequestData);
            if (messageResponse != null)
                if(messageResponse.Success)
                {
                    _request.CertRequest = messageResponse.Data.value;
                    _request.StepId = 4;
                    _request.ContainerName = certRequestData.ContainerName;
                    await SilentUpdate();
                    NavigationManager.NavigateTo("/sendRequestConfirm");
                }
                else
                {
                    var parameters = new DialogParameters();
                    parameters.Add("ContentText", messageResponse.Message);
                    parameters.Add("ButtonText", "OK");
                    parameters.Add("Color", Color.Error);

                    var dialog = DialogService.Show<ErrorDialog>("Ошибка", parameters);
                    var result = await dialog.Result;
                }
                    
                
                    
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


        //Загрузка Сертификата
        private async Task OnInputFileChangedCert(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 6;
            await ClearCert();
            ClearDragClassCert();
            var file = e.File;
            fileNamesCert.Add(file.Name);
            await certValidation.Validate();
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDtoCert = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDtoCert);  
            }
            
        }

        private async Task ClearCert()
        {
            if (RequestFileReadDtoCert.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDtoCert.Id);
                RequestFileReadDtoCert.Id = Guid.Empty;
            }
            
            fileNamesCert.Clear();
            ClearDragClassCert();

        }

        private void SetDragClassCert()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClassCert()
        {
            DragClass = DefaultDragClass;
        }

        private string ValidationCert(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNamesCert.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
        }

        public void Dispose()
        {
            Interceptor.DisposeEvent();
        }

        protected async Task CertBlanckPrint() 
        {
           

                var fileBytes = await PdfGeneratorHttpRepo.GenerateCertBlanck(_request.Id); 
                var fileName = $"Бланк сертификата {_request.PersonLastName} {_request.PersonFirstName} {_request.PersonPatronymic}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
           
        }

        //Загрузка Бланка сертификата>
        private async Task OnInputFileChangedCertBlanck(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 7;
            await ClearCertBlanck();
            ClearDragClassCertBlanck();
            var file = e.File;
            fileNamesCertBlanck.Add(file.Name);
            await certBlanckValidation.Validate();
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDtoCertBlanck = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDtoCertBlanck);  
            }
            
        }

        private async Task ClearCertBlanck()
        {
            if (RequestFileReadDtoCertBlanck.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDtoCertBlanck.Id);
                RequestFileReadDtoCertBlanck.Id = Guid.Empty;
            }
            
            fileNamesCertBlanck.Clear();
            ClearDragClassCertBlanck();

        }

        private void SetDragClassCertBlanck()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClassCertBlanck()
        {
            DragClass = DefaultDragClass;
        }

        private string ValidationCertBlanck(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNamesCertBlanck.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
        }



    }
}