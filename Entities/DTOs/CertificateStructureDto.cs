using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.DTOs
{
    public class CertificateStructureDto
    {
        public string? SerialNumber { get; set; }
        public string? NotBefore { get; set; }
        public string? NotAfter { get; set; }
        public string? FirstName { get; set; }
        public string? GivenName { get; set; }
        public string? Snils { get; set; }
        public string? PersonInn { get; set; }
        public string? Organization { get; set; }
        public string? Inn { get; set; }
        public string? Email { get; set; }
        public string? IdentificationKind { get; set; } 
        public string? IdentificationKindCode { get; set; }
        public string? AuthorityName { get; set; }
        public string? AuthorityAddressStreet { get; set; }
        public string? AuthorityAddressCity { get; set; }
        public string? AuthoritySerialNumber { get; set; }
        public string? AuthoritySignTool { get; set; }
        
        public string? AuthoritySignToolCertificate { get; set; }
        public string? AuthorityCaTool { get; set; }
        public string? AuthorityCaCertificate { get; set; }
        public string? AuthorityType { get; set; } = "КС2";
        public string? CertAlgorithm { get; set; }
        public string? CertSigAlgOid { get; set; }
        public string? CertSignTool { get; set; }
        public string? CertSignType { get; set; } = "Класс средства ЭП КС1, Класс средства ЭП КС2";
        public string? CertKeyUsage { get; set; } = "Цифровая подпись, Неотрекаемость, Шифрование ключей, Шифрование данных";
        public string? PublicKey { get; set; }
        public string? SignatureAlgorithm { get; set; }
        public string? Signature { get; set; }
    }
}