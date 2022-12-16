using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using Client.Features;

namespace Client.HttpRepository
{
    public interface IRegRequestHttpRepository
    {
        Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent);
        Task<PagingResponse<RequestAbonent>> GetRequestAbonents(RequestAbonentParameters requestAbonentParameters);
        Task<PagingResponse<RequestAbonent>> GetDraftRequestAbonents(RequestAbonentParameters requestAbonentParameters);
        Task<RequestAbonentReadDto> GetRequestAbonent(Guid id);
        Task CreateRequestAbonent(RequestAbonentCreateDto requestAbonentCreateDto);
        Task CreateRequestAbonents(RequestAbonentListDto requestAbonentListDto);
        Task UpdateRequestAbonent(Guid id, RequestAbonentUpdateDto requestAbonentUpdateDto);
        Task DeleteRequestAbonent(Guid id);
    }
}