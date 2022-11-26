using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Reg.Server.Context;
using Microsoft.EntityFrameworkCore;

namespace Server.Repository
{
    public class ProviderRepository : IProviderRepository
    {
        private readonly RegContext _context;
        public ProviderRepository(RegContext context)
        {
            _context = context;
        }
        public async Task<Provider> GetProvider(int providerId)
        {
            return await _context.Providers.FindAsync(providerId);
        }

        public async Task<int> GetProviderId(string providerName)
        {
            var provider = await _context.Providers.FirstOrDefaultAsync(p => p.ProviderName == providerName);
            
            return provider.ProviderId;
        }

        public IQueryable<Provider> GetProviders()
        {
            return  _context.Providers.AsQueryable();
        }
    }
}