using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Server.Repository
{
    public interface IRegionRepository
    {
        Task<Region> GetRegion(int regionId);

        Task<Region> GetRegion(string regionCode);
    }
}