using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CountryReadDto
    {
        public int global_id { get; set; }
        public string signature_date { get; set; } = string.Empty;
        public string system_object_id { get; set; } = string.Empty;
        public string ALFA3 { get; set; } = string.Empty;
        public string SHORTNAME { get; set; } = string.Empty;
        public string FULLNAME { get; set; } = string.Empty;
        public string ALFA2 { get; set; } = string.Empty;
        public string CODE { get; set; } = string.Empty;
    }
}