using Client.HttpRepository;
using Entities.Models;
using Microsoft.AspNetCore.Components;
using Entities.RequestFeatures;
using MudBlazor;
using Client.Features;

namespace Reg.Client.Pages
{
    public partial class Requests
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

        private async Task<TableData<RequestAbonent>> GetServerData(TableState state)
		{
			_requestAbonentParameters.PageSize = state.PageSize;
			_requestAbonentParameters.PageNumber = state.Page + 1;

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
			await RequestRepo.DeleteRequestAbonent(id);

            if (_requestAbonentParameters.PageNumber > 1 && RequestAbonentList.Count == 1) //разобраться почему не работает Приудалении последнего запроса номер страницы должен изменяться на единицу
            {
                _requestAbonentParameters.PageNumber--;
            }

            await _table.ReloadServerData();
		}

        public void RowClicked(TableRowClickEventArgs<RequestAbonent> p)
        {
            NavigationManager.NavigateTo($"/request/{p.Item.Id}");
        }

        private async void OnButtonClicked()
        {
            bool? result = await DialogService.ShowMessageBox(
                "Предупреждение", 
                "Вы действительно хотите удалить заявление?", 
                yesText:"Delete!", cancelText:"Cancel");
            state= result==null ? "Cancelled" : "Deleted!";
            if (state == "Deleted!")
            {
                
            }
            StateHasChanged();
        }
    }
}