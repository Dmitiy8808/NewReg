using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Reg.Server.Context;
using Server.Paging;
using Entities.RequestFeatures;
using Server.Repository.RepositoryEtensions;

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
                    .Include(le => le.Leader)
                    .Include(f => f.Files)
                    .FirstOrDefaultAsync(req => req.Id == id);
        }

        public async Task<PagedList<RequestAbonent>> GetRequests(RequestAbonentParameters requestAbonentParameters)
        {
           var requests =  await _context.Requests
                .Include(c => c.Person)
                .Where(s => s.StepId != 1)
                .Search(requestAbonentParameters.SearchTerm)
				.Sort(requestAbonentParameters.OrderBy)
                .ToListAsync();

           return PagedList<RequestAbonent>
                .ToPagedList(requests, requestAbonentParameters.PageNumber, requestAbonentParameters.PageSize);

        }

        public async Task<PagedList<RequestAbonent>> GetDraftRequests(RequestAbonentParameters requestAbonentParameters)
        {
           var requests =  await _context.Requests
                .Include(c => c.Person)
                .Where(s => s.StepId == 1)
                .Search(requestAbonentParameters.SearchTerm)
                .Sort(requestAbonentParameters.OrderBy)
                .ToListAsync();

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