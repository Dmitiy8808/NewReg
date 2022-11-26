using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace Reg.Client.HttpRepository
{
    public class RegRequestHttpRepository : IRegRequestHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        public RegRequestHttpRepository( HttpClient client, IMapper mapper)
        {
            _mapper = mapper;
            _client = client;
        }
        public async Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent)
        {
            var response = await _client.PostAsJsonAsync<RequestAbonent>("regrequests/getRequestData", clientAbonent);
            response.EnsureSuccessStatusCode();
            CertRequestData certRequestData = await response.Content.ReadFromJsonAsync<CertRequestData>();
            var certRequestDataDto = _mapper.Map<CertRequestDataDto>(certRequestData);
            return certRequestDataDto;
        }
    }
}