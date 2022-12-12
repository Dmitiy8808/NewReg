using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RequestFileReadDto
    {
        public Guid Id { get; set; }
        public int TypeId { get; set; }
        public string? Name { get; set; }
    }
}