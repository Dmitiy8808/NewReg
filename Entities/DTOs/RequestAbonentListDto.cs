using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class RequestAbonentListDto
    {
        public Guid Id {get; set;}
        public List<RequestAbonentCreateDto> AbonentList { get; set; }
        public RequestAbonentListDto()
        {
            Id = Guid.NewGuid();
            AbonentList = new List<RequestAbonentCreateDto>();
        }
    }
}


