using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Reg.Server.Context;

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
            _context.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<RequestAbonent> GetRequest(Guid id)
        {
            return await _context.Requests.FindAsync(id);
        }

        public async Task<List<RequestAbonent>> GetRequests()
        {
            return await _context.Requests.ToListAsync();
        }
    }
}