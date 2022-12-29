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
    public partial class UpdateRequest : IDisposable
    {
        MudForm form; 
        MudForm formdoc;
        bool dataSuccess;
        bool docSuccess;
        MudFileUpload<IReadOnlyList<IBrowserFile>> passportValidation; 
        MudFileUpload<IReadOnlyList<IBrowserFile>> snilsValidation; 
        MudFileUpload<IReadOnlyList<IBrowserFile>> claimValidation; 
        MudFileUpload<IReadOnlyList<IBrowserFile>> dovValidation;
        MudFileUpload<IReadOnlyList<IBrowserFile>> selfiValidation;
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
        IDialogService DialogService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Inject]
        IPdfGeneratorHttpRepository PdfGeneratorHttpRepo { get; set; }
        [Inject]
        IRequestFileHttpRepository RequestFileHttpRepo { get; set; }
        [Inject]
        IRegRequestHttpRepository RequestRepo { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        IMapper Mapper { get; set; }
        [Inject] 
        private HttpInterceptorService Interceptor { get; set; }
        [Inject]
        public IWebSocketService WebSocketService { get; set; }
        [Inject]
        public ICountryHttpRepository CountryHttpRepository { get; set; }
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
        public RequestFileReadDto RequestFileReadDtoSelfi = new RequestFileReadDto();
        private RequestFileReadDto? _dovFile { get; set; }
        private RequestFileReadDto? _selfiFile { get; set; }
        public RequestFileReadDto RequestFileReadDtoClaim = new RequestFileReadDto();
        private RequestFileReadDto? _claimFile { get; set; }
        List<RequestFileReadDto> requestFileList= new List<RequestFileReadDto>();
        private static string DefaultDragClass = "relative rounded-lg border-2 border-dashed pa-4 mt-4 mud-width-full mud-height-full ";
        private string DragClass = DefaultDragClass;
        private List<string> fileNames = new List<string>();
        private List<string> fileNamesSnils = new List<string>();
        private List<string> fileNamesDov = new List<string>();
        private List<string> fileNamesSelfi = new List<string>();
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
            }
            _snilsFile = requestFileList.Where(type => type.TypeId == 2).FirstOrDefault();
            if (_snilsFile != null)
            {
               fileNamesSnils.Add(_snilsFile.Name); 
               RequestFileReadDtoSnils.Id = _snilsFile.Id;
            }

            _dovFile = requestFileList.Where(type => type.TypeId == 4).FirstOrDefault();
            if (_dovFile != null)
            {
               fileNamesDov.Add(_dovFile.Name); 
               RequestFileReadDtoDov.Id = _dovFile.Id;
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
            Snackbar.Add("Заявка сохранена!", Severity.Success);
           
        }

        private async Task SilentUpdate()
        {   
            
            await RequestRepo.UpdateRequestAbonent(Id, Mapper.Map<RequestAbonentUpdateDto>(_request));
           
        }

        private async Task Back()
        {   
            NavigationManager.NavigateTo($"/connection/{Search}", false);
        }

        private async Task GenerateRequest()
        {
            await form.Validate();
            await formdoc.Validate();
            if (dataSuccess & docSuccess)
            {
                await SilentUpdate();

                await Confirm();
            }
            else if (!docSuccess)
            {
                Snackbar.Add("Загрузите документы", Severity.Error);
            }
    

            // var mapReqAbon = Mapper.Map<RequestAbonent>(_request);
            
            // await WebSocketService.GenerateRequest(mapReqAbon);
            // Snackbar.Add("Запрос успешно сформирован!", Severity.Success);
           
        }

        private async Task RequestComplited()
        {
            await form.Validate();
            await formdoc.Validate();
            if (dataSuccess & docSuccess)
            {

                
                await ConfirmComplite();
            }
            else if (!docSuccess)
            {
                Snackbar.Add("Загрузите документы", Severity.Error);
            }
           
        }

        private string ValidationPass(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNames.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
        } 

        private string ValidationSelfi(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNamesSelfi.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
        }

        private string ValidationSnils(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNamesSnils.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
        }  

        private string ValidationClaim(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNamesClaim.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
        }   

        private string ValidationDov(IReadOnlyList<IBrowserFile> fileList)
        {
            if (!fileNamesDov.Any())
            {
               return "Загрузите документ"; 
            }
            return null;
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

        // void GenderChanged(string selectedOption)
        // {
        //     if(selectedOption == "1")
        //     {
        //         _request.PersonGender = 1;
        //     }
        //     else if (selectedOption == "2")
        //     {
        //         _request.PersonGender = 2;
        //     }
        // }

//Загрузка СНИЛС
        private async Task OnInputFileChangedSnils(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 2;
            await ClearSnils();
            ClearDragClassSnils();
            var file = e.File;
            fileNamesSnils.Add(file.Name);
            await snilsValidation.Validate();
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDtoSnils = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDtoSnils);  
            }
            
        }

        private async Task ClearSnils()
        {
            if (RequestFileReadDtoSnils.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDtoSnils.Id);
                RequestFileReadDtoSnils.Id = Guid.Empty;
            }
            
            fileNamesSnils.Clear();
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
            await passportValidation.Validate();
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

        //Загрузка Доверенность 
        private async Task OnInputFileChangedDov(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 4;
            await ClearDov();
            ClearDragClassDov();
            var file = e.File;
            fileNamesDov.Add(file.Name);
            await dovValidation.Validate();
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDtoDov = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDtoDov);  
            }
            
        }

        private async Task ClearDov()
        {
            if (RequestFileReadDtoDov.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDtoDov.Id);
                RequestFileReadDtoDov.Id = Guid.Empty;
            }
            
            fileNamesDov.Clear();
            ClearDragClassDov();

        }

        private void SetDragClassDov()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClassDov()
        {
            DragClass = DefaultDragClass;
        }

        //Загрузка Заявление
        private async Task OnInputFileChangedClaim(InputFileChangeEventArgs e)
        {
            requestFileFeatures.TypeId = 3;
            await ClearClaim();
            ClearDragClassClaim();
            var file = e.File;
            fileNamesClaim.Add(file.Name);
            await claimValidation.Validate();
            using (var ms = file.OpenReadStream(file.Size))
            {
                var content = new MultipartFormDataContent();
                content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
                content.Add(new StreamContent(ms, Convert.ToInt32(file.Size)), file.ContentType, file.Name);
                RequestFileReadDtoClaim = await RequestFileHttpRepo.UploadRequestFile(content, requestFileFeatures);
                await OnChange.InvokeAsync(RequestFileReadDtoClaim);  
            }
            
        }

        private async Task ClearClaim()
        {
            if (RequestFileReadDtoClaim.Id != Guid.Empty)
            {
                await RequestFileHttpRepo.DeleteRequestFile(RequestFileReadDtoClaim.Id);
                RequestFileReadDtoClaim.Id = Guid.Empty;
            }
            
            fileNamesClaim.Clear();
            ClearDragClassClaim();

        }

        private void SetDragClassClaim()
        {
            DragClass = $"{DefaultDragClass} mud-border-primary";
        }

        private void ClearDragClassClaim()
        {
            DragClass = DefaultDragClass;
        }

        protected async Task ClaimPrint() 
        {
            await form.Validate();
            
            if (dataSuccess)
            {
                var fileBytes = await PdfGeneratorHttpRepo.GenerateClaim(Mapper.Map<RequestAbonentUpdateDto>(_request)); 
                var fileName = $"Заявление {_request.PersonLastName} {_request.PersonFirstName} {_request.PersonPatronymic}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
                await SilentUpdate();
            }
            
        }

        protected async Task DoverPrint() 
        {
            await form.Validate();
            
            if (dataSuccess)
            {
                var fileBytes = await PdfGeneratorHttpRepo.GenerateDover(Mapper.Map<RequestAbonentUpdateDto>(_request)); 
                var fileName = $"Доверенность {_request.PersonLastName} {_request.PersonFirstName} {_request.PersonPatronymic}.pdf";
                await JSRuntime.InvokeAsync<object>("saveAsFile", fileName, Convert.ToBase64String(fileBytes));
                await SilentUpdate();
            }
        }

        private async Task Confirm()
        {
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Заявление будет закрыто для редактирования и получит статус «Генерация запроса» Больше не будет возможности внести изменения в заявление. Нажимая кнопку \"Отправить\" Вы подтверждаете идентификацию личности лица указанного в заявке");
            parameters.Add("ButtonText", "Отправить");
            parameters.Add("Color", Color.Primary);

            var dialog = DialogService.Show<ConfirmDialog>("Подготовка к формированию контейнера", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                Console.WriteLine("Подтверждение диалога");
                _request.StepId = 3;
                await SilentUpdate();
                NavigationManager.NavigateTo("/generateRequestConfirm");
            }
        }

         private async Task ConfirmComplite()
        {
            // var parameters = new DialogParameters();
            // parameters.Add("ContentText", "Заявление будет передано на рассотрение администратору вашей организации. ");
            // parameters.Add("ButtonText", "Отправить");
            // parameters.Add("Color", Color.Primary);

            // var dialog = DialogService.Show<ConfirmDialog>("Подготовка заявленя", parameters);
            // var result = await dialog.Result;
            // if (!result.Cancelled)
            // {
                Console.WriteLine("Подтверждение диалога");
                _request.StepId = 2;
                await SilentUpdate();
                NavigationManager.NavigateTo("/draftSave");
            // }
        }

        public void Dispose()
        {
            Interceptor.DisposeEvent();
        }
    }
}