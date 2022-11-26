using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Region
    {
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public string RegionCode { get; set; }
        public string? RegionPfrCode { get; set; }
        public int OfficeId { get; set; }
        public int TariffZoneId { get; set; } = 1;
    }
}