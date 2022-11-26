using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;

namespace Server.Services
{
    public interface IQualifiedCertificateManager
    {
        Task<CertRequestData> GetCertificateRequestData(RequestAbonent clientAbonent);
    }
}