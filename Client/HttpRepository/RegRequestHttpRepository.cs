using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Client.Features;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestFeatures;
using Microsoft.AspNetCore.WebUtilities;

namespace Client.HttpRepository
{
    public class RegRequestHttpRepository : IRegRequestHttpRepository
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;
        private readonly JsonSerializerOptions _options =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public RegRequestHttpRepository( HttpClient client, IMapper mapper)
        {
            _mapper = mapper;
            _client = client;
        }

        public Task CreateRequestAbonent(RequestAbonentCreateDto requestAbonentCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<CertRequestDataDto> GetCertRequestData(RequestAbonent clientAbonent)
        {
            var response = await _client.PostAsJsonAsync<RequestAbonent>("regrequests/getRequestData", clientAbonent);
            response.EnsureSuccessStatusCode();
            CertRequestData certRequestData = await response.Content.ReadFromJsonAsync<CertRequestData>();
            var certRequestDataDto = _mapper.Map<CertRequestDataDto>(certRequestData);
            return certRequestDataDto;
        }

        public async Task<RequestAbonentReadDto> GetRequestAbonent(Guid id)
        {
            var requestAbonent = await _client.GetFromJsonAsync<RequestAbonentReadDto>($"regrequests/RequestAbonent/{id}");
            return requestAbonent;
        }

        public async Task<PagingResponse<RequestAbonent>> GetRequestAbonents(RequestAbonentParameters requestAbonentParameters)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                
                ["pageNumber"] = requestAbonentParameters.PageNumber.ToString(),
                ["pageSize"] = requestAbonentParameters.PageSize.ToString()
            };
            var response = await _client.GetAsync(QueryHelpers
                            .AddQueryString("regrequests/RequestAbonent", queryStringParam)); 

            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException(content);
            }

            var pagingResponse = new PagingResponse<RequestAbonent>
            {
                Items = JsonSerializer.Deserialize<List<RequestAbonent>>(content, _options),
                MetaData = JsonSerializer.Deserialize<MetaData>(
                    response.Headers.GetValues("X-Pagination").First(), _options)
            };

            return pagingResponse;
        }

        public async Task UpdateRequestAbonent(Guid id, RequestAbonentUpdateDto requestAbonentUpdateDto)
        {
            await _client.PutAsJsonAsync($"regrequests/RequestAbonent/{id}", requestAbonentUpdateDto);
        }

        public async Task DeleteRequestAbonent(Guid id)
        {
            await _client.DeleteAsync($"regrequests/RequestAbonent/{id}");
        }
    }
}