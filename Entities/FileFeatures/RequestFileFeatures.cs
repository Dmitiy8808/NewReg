using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.FileFeatures
{
    public class RequestFileFeatures
    {
        public int TypeId { get; set; }
        public Guid RequestAbonentId { get; set; }
    }
}