using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{

    public class CertRequestData
    {
        public CertRequestDataCryptoProvider Provider { get; set; }

        public string SignTool { get; set; }

        public CertAttribute[] CertAttributes { get; set; }

        public string[] EnhKeyUsage { get; set; }

        public CertAttribute[] CertPolicies { get; set; }

        public CertAltarnativeName[] CertAltarnativeNames { get; set; }

        public int KeyUsage { get; set; }

        public string ContainerName { get; set; }

        public string NotBefore { get; set; }

        public string NotAfter { get; set; }

        public int? IdentificationKind { get; set; }

        public CertRequestData()
        {
        this.CertAttributes = new CertAttribute[0];
        this.EnhKeyUsage = new string[0];
        this.CertPolicies = new CertAttribute[0];
        this.CertAltarnativeNames = new CertAltarnativeName[0];
        }
    }
    
}