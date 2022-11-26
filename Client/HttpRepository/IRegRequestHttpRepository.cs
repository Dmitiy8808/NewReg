using Entities.DTOs;
using Entities.Models;

namespace Reg.Client.HttpRepository
{
    public interface IRegRequestHttpRepository
    {
        Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent);
    }
}