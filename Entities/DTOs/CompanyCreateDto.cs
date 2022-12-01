using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Entities.DTOs
{
    public class CompanyCreateDto
    {
        public string? Inn { get; set; }

        public string? Kpp { get; set; }

        public string? Ogrn { get; set; }

        public string? ShortName { get; set; }
        
        public string? LocationAddressPostalCode { get; set; }

        public int LocationAddressRegionId { get; set; }

        public string? LocationAddressRegionCode { get; set; }

        public string? LocationAddressArea { get; set; }

        public string? LocationAddressCity { get; set; }

        public string? LocationAddressLocality { get; set; }

        public string? LocationAddressStreet { get; set; }

        public string? LocationAddressBuilding { get; set; }

        public string? LocationAddressBulk { get; set; }

        public string? LocationAddressFlat { get; set; }
    }
}