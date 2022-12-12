
using Entities.Models;

namespace Client.HttpRepository
{
    public interface ICountryHttpRepository
    {
        Task<List<Country>> GetCountries();
    }
}