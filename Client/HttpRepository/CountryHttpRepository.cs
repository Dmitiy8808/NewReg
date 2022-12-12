using System.Net.Http.Json;
using Entities.Models;

namespace Client.HttpRepository
{
    public class CountryHttpRepository : ICountryHttpRepository
    {
        private readonly HttpClient _client;
        public CountryHttpRepository(HttpClient client)
        {
            _client = client;
            
        }
        public async Task<List<Country>> GetCountries()
        {
            return await _client.GetFromJsonAsync<List<Country>>("country");
        }
    }
}