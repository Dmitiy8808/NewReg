using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class QualifiedCertificateVolatile
    {
        public static readonly string[] EnhKeyUsage = new string[2]
        {
        "1.3.6.1.5.5.7.3.2",
        "1.3.6.1.5.5.7.3.4"
        };
        public static readonly CertAttribute[] CertPolicies = new CertAttribute[2]
        {
        new CertAttribute()
        {
            Oid = "1.2.643.100.113.1",
            Value = string.Empty
        },
        new CertAttribute()
        {
            Oid = "1.2.643.100.113.2",
            Value = string.Empty
        }
        };
        public static readonly int KeyUsage = 240;
        public static readonly string Country = "RU";
        public static readonly string AbsentSnils = "00000000000";
        public static readonly string AbsentOgrn = "0000000000000";
        public static readonly int RnsFssAltarnativeNameType = 5;
        }
}