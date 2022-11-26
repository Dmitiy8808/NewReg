using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class AddressInfo
    {
        public int Id { get; set; }
        public string? PostalCode { get; set; }

        public int RegionId { get; set; }

        public string? RegionCode { get; set; }

        public string? Area { get; set; }

        public string? City { get; set; }

        public string? Locality { get; set; }

        public string? Street { get; set; }

        public string? Building { get; set; }

        public string? Bulk { get; set; }

        public string? Flat { get; set; }
        // public Guid RequestAbonentId { get; set; }
        // public RequestAbonent? RequestAbonent { get; set; }
    }
}