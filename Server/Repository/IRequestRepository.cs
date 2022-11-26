using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Server.Repository
{
    public interface IRequestRepository
    {
        Task<List<RequestAbonent>> GetRequests();
        Task<RequestAbonent> GetRequest(Guid id);
        Task CreateRequest(RequestAbonent request);
    }
}