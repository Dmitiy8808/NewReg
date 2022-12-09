using Server.Paging;
using Entities.RequestFeatures;
using Entities.Models;

namespace Server.Repository
{
    public interface IRequestRepository
    {
        Task<PagedList<RequestAbonent>> GetRequests(RequestAbonentParameters requestAbonentParameters);
        Task<RequestAbonent> GetRequest(Guid id);
        Task CreateRequest(RequestAbonent request);
        Task CreateRequests(List<RequestAbonent> requests);
        Task DeleteRequest(RequestAbonent request);
        void UpdateRequest(RequestAbonent request);
        Task SaveAsync();
    }
}