using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTOs;
using Entities.Models;

namespace Server.Services
{
    public interface IQualifiedCertificateManager
    {
        Task<CertRequestData> GetCertificateRequestData(RequestAbonent clientAbonent);
        Task<CertificateStructureDto> GetCertificateData(Guid id);
    }
}