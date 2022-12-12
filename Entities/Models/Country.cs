using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string CountryName { get; set; } = string.Empty;
    }
}