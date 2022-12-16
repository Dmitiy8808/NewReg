using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Entities.DTOs;
using Entities.FileFeatures;
using Entities.Models;
using Microsoft.AspNetCore.WebUtilities;

namespace Client.HttpRepository
{
    public class RequestFileHttpRepository : IRequestFileHttpRepository
    {
        private readonly HttpClient _client;
        public RequestFileHttpRepository(HttpClient client)
        {
            _client = client;
            
        }

        public async Task DeleteRequestFile(Guid id)
        {
            await _client.DeleteAsync($"file/{id}");
        }

        public async Task<byte[]> GetRequestFile(Guid id)
        {
            var request = await _client.GetFromJsonAsync<RequestFile>($"file/{id}");
            
            return request.Data;
        }

        public async Task<List<RequestFileReadDto>> GetRequestFiles(Guid id)
        {
            return await _client.GetFromJsonAsync<List<RequestFileReadDto>>($"file/list/{id}");
        }

        public async Task<RequestFileReadDto> UploadRequestFile(MultipartFormDataContent content, RequestFileFeatures requestFileFeatures)
        {
            var queryStringParam = new Dictionary<string, string>
            {
                
                ["typeId"] = requestFileFeatures.TypeId.ToString(),
                ["requestAbonentId"] = requestFileFeatures.RequestAbonentId.ToString()
            };
            var requestUri = QueryHelpers.AddQueryString("file", queryStringParam);

            // var request = new HttpRequestMessage(HttpMethod.Post, requestUri);
            // request.Content = content;

            var postResult = await _client.PostAsync(requestUri, content);
            var postContent = await postResult.Content.ReadFromJsonAsync<RequestFileReadDto>();
            if (!postResult.IsSuccessStatusCode)
            {
                throw new ApplicationException("Неудачная загрузка файла");
            }
            else
                return postContent;
        }
        
    }
}