using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class RequestFile
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte[]? Data { get; set; }

        public Guid RequestAbonentId { get; set; }
        public RequestAbonent? RequestAbonent { get; set; } 
    }

    // 1 - Уд личности; 2 - СНИЛС; 
}