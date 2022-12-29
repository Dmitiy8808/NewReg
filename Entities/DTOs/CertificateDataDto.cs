using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CertificateDataDto
    {
        public bool IsError { get; set; }
        public int ProviderCode { get; set; }
        public string? ProviderName { get; set; }
        public string? ContainerName { get; set; }
        public string? CertData { get; set; }
    }
}