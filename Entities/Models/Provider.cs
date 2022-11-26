using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Provider
    {
        public int ProviderId { get; set; }
        public string ProviderName { get; set; }
        public int ProviderType { get; set; }
        public DateTime CreationTime { get; set; }
        public string  SignTool { get; set; }
    }
}