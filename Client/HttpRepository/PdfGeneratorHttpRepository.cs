using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Client.HttpRepository
{
    public class PdfGeneratorHttpRepository : IPdfGeneratorHttpRepository
    {
        private readonly HttpClient _client;
        public PdfGeneratorHttpRepository(HttpClient client)
        {
            _client = client;
            
        }
        public async Task<byte[]> GenerateClaim(RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            var response = await _client.PostAsJsonAsync<RequestAbonentUpdateDto>("pdfcreator", requestAbonentUpdateDto);
            return await response.Content.ReadAsByteArrayAsync(); 
        }

        public async Task<byte[]> GenerateDover(RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            var response = await _client.PostAsJsonAsync<RequestAbonentUpdateDto>("pdfcreator/dover", requestAbonentUpdateDto);
            return await response.Content.ReadAsByteArrayAsync(); 
        }

        public async Task<byte[]> GenerateCertBlanck(Guid id)
        {
            var response = await _client.GetByteArrayAsync($"pdfcreator/cert/{id}");
            return response; 
        }
    }
}