using System.Collections.Generic;

namespace Entities.Models
{
    public class RequestList
    {
         public int Id {get; set;}
         public List<RequestAbonent> AbonentList { get; set; }
    }
}