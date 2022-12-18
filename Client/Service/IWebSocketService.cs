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
        Task<MessageResponse> SendMessage(string message);
        Task<MessageResponse> GenerateRequest(RequestAbonent requestAbonent);
        Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent);
    }
}