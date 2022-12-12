using Entities.Models;

namespace Server.Repository
{
    public interface ICountryRepository
    {
        Task<List<Country>> GetCountries();
        Task CreateCountry(Country country);
        Task CreateCountries(List<Country> country);
        Task<Country> GetCountry(int id);
    }
}