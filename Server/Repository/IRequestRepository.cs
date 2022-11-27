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
        void UpdateRequest(RequestAbonent request);
        public bool SaveChanges();
    }
}