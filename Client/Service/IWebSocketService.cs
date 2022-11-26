using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTOs;
using Entities.Models;

namespace Client.Service
{
    public interface IWebSocketService
    {
        Task<string> SendMessage(string message);
        Task GenerateRequest();
        Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent);
    }
}