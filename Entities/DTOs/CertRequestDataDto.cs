using Entities.Models;

namespace Entities.DTOs
{

    public class CertRequestDataDto
    {
        public string providerName { get; set; }

        public uint providerCode { get; set; }

        public string SignTool { get; set; }

        public CertAttribute[] CertAttributes { get; set; }

        public string[] EnhKeyUsage { get; set; }

        public CertAttribute[] CertPolicies { get; set; }

        public CertAltarnativeName[] CertAltarnativeNames { get; set; }

        public int KeyUsage { get; set; }

        public string ContainerName { get; set; }

        public string RequestName { get; set; }

        public string NotBefore { get; set; }

        public string NotAfter { get; set; }

        public int IdentificationKind { get; set; }

    }
    
}