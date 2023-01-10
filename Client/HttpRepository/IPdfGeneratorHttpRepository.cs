using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Client.HttpRepository
{
    public interface IPdfGeneratorHttpRepository
    {
        Task<byte[]> GenerateClaim(RequestAbonentUpdateDto requestAbonentUpdateDto);
        Task<byte[]> GenerateDover(RequestAbonentUpdateDto requestAbonentUpdateDto);
        Task<byte[]> GenerateCertBlanck(Guid id);
    }
}