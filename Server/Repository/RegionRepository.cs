using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Reg.Server.Context;

namespace Server.Repository
{
    public class RegionRepository : IRegionRepository
    {
        private readonly RegContext _context;
        public RegionRepository(RegContext context)
        {
            _context = context;
        }
        
        public async Task<Region> GetRegion(int regionId)
        {
            return await _context.Regions.FindAsync(regionId);
        }

        public async Task<Region> GetRegion(string regionCode)
        {
            return await _context.Regions.FirstOrDefaultAsync(r => r.RegionCode == regionCode);
        }

    }
}