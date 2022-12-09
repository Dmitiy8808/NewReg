using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Reg.Server.Context;
using Server.Paging;
using Entities.RequestFeatures;

namespace Server.Repository
{
    public class RequestRepository : IRequestRepository
    {
        private readonly RegContext _context;
        public RequestRepository(RegContext context)
        {
            _context = context;
        }

        public async Task CreateRequest(RequestAbonent request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }
            _context.Requests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<RequestAbonent> GetRequest(Guid id)
        {
            return await _context.Requests
                    .Include(c => c.Person)
                    .Include(la => la.LocationAddress)
                    .FirstOrDefaultAsync(req => req.Id == id);
        }

        public async Task<PagedList<RequestAbonent>> GetRequests(RequestAbonentParameters requestAbonentParameters)
        {
           var requests =  await _context.Requests.ToListAsync();

           return PagedList<RequestAbonent>
                .ToPagedList(requests, requestAbonentParameters.PageNumber, requestAbonentParameters.PageSize);

        }

     
        public void UpdateRequest(RequestAbonent request)
        {
            //Nothing Implement by Automapper
        }

        public async Task DeleteRequest(RequestAbonent request)
        {
            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            _context.Requests.Remove(request);
            
            await _context.SaveChangesAsync();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CreateRequests(List<RequestAbonent> requests)
        {
            if(requests == null)
            {
                throw new ArgumentNullException(nameof(requests));
            }
            await _context.Requests.AddRangeAsync(requests);
            await _context.SaveChangesAsync();
        }
    }
}