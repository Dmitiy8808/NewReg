using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Company
    {
        public Guid Id { get; set; }
        public string? Inn { get; set; }

        public string? Kpp { get; set; }

        public string? Ogrn { get; set; }

        public string? ShortName { get; set; }
        public AddressInfo? LocationAddress { get; set; }
    }
}