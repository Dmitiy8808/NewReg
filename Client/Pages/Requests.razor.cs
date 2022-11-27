using Reg.Client.HttpRepository;
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
        public List<RequestAbonent> RequestAbonentList { get; set; } = new List<RequestAbonent>();
        public MetaData MetaData { get; set; } = new MetaData();

        private RequestAbonentParameters _requestAbonentParameters = new RequestAbonentParameters();
        [Inject]
        public IRegRequestHttpRepository RequestRepo { get; set; }
        private PagingResponse<RequestAbonent> _response;
        private MudTable<RequestAbonent> _table;
        private readonly int[] _pageSizeOption = { 15, 25, 50};

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

        private void Delete(Guid id)
		{
			
		}

        public void RowClicked(TableRowClickEventArgs<RequestAbonent> p)
        {
            NavigationManager.NavigateTo($"/request/{p.Item.Id}");
        }
    }
}