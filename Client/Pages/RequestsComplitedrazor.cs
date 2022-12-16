using Client.HttpRepository;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using Entities.RequestFeatures;
using MudBlazor;
using Client.Features;

namespace Reg.Client.Pages
{
    public partial class RequestsComplited
    {
        [Inject]
		public NavigationManager NavigationManager { get; set; }
        [Inject] 
        private IDialogService DialogService { get; set; }
        public List<RequestAbonent> RequestAbonentList { get; set; } = new List<RequestAbonent>();
        public MetaData MetaData { get; set; } = new MetaData();

        private RequestAbonentParameters _requestAbonentParameters = new RequestAbonentParameters();
        [Inject]
        public IRegRequestHttpRepository RequestRepo { get; set; }
        private PagingResponse<RequestAbonent> _response;
        private MudTable<RequestAbonent> _table;
        private readonly int[] _pageSizeOption = { 15, 25, 50};
        string state = string.Empty;

        Dictionary<int, string> status = new Dictionary<int, string>()
        {
            [1] = "Черновик",

            [2] = "Подготовлено",

            [3] = "Подготовлено",

            [4] = "Генерация запроса",

            [5] = "Ожидание сертификата",

            [6] = "Установка сертификата",

            [7] = "Выполнено",
        };

        private async Task<TableData<RequestAbonent>> GetServerData(TableState state)
		{
			_requestAbonentParameters.PageSize = state.PageSize;
			_requestAbonentParameters.PageNumber = state.Page + 1;

            _requestAbonentParameters.OrderBy = state.SortDirection == SortDirection.Descending ?
            state.SortLabel + " desc" :
            state.SortLabel;

			_response = await RequestRepo.GetRequestAbonents(_requestAbonentParameters);

            

			return new TableData<RequestAbonent>
			{
				Items = _response.Items,
				TotalItems = _response.MetaData.TotalCount
			};
		}

        protected async override Task OnInitializedAsync()
        {
          
        }

        private async void Delete(Guid id)
		{
            var parameters = new DialogParameters();
            parameters.Add("ContentText", "Вы действительно хотите удалить заявление?");
            parameters.Add("ButtonText", "Удалить");
            parameters.Add("Color", Color.Warning);
            var dialog = DialogService.Show<ConfirmDeleteDialog>("Подтверждение", parameters);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                await RequestRepo.DeleteRequestAbonent(id);
            }
            if (_requestAbonentParameters.PageNumber > 1 && RequestAbonentList.Count == 1) //разобраться почему не работает Приудалении последнего запроса номер страницы должен изменяться на единицу
            {
                _requestAbonentParameters.PageNumber--;
            }

            await _table.ReloadServerData();
		}

        public void RowClicked(TableRowClickEventArgs<RequestAbonent> p)
        {
            NavigationManager.NavigateTo($"/requestGenerate/{p.Item.Id}");
        }

        private void OnSearch(string searchTerm)
        {
            _requestAbonentParameters.SearchTerm = searchTerm;
            _table.ReloadServerData();
        }

  
    }
}