using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Client.HttpRepository
{
    public class CompanyHttpRepository : ICompanyHttpRepository
    {
        private readonly HttpClient _client;
        public CompanyHttpRepository(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<CompanyReadDto>> GetCompanies()
        {
            return await _client.GetFromJsonAsync<List<CompanyReadDto>>("company");
        }
    }
}