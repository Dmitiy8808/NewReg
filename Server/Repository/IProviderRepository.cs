using Entities.Models;

namespace Server.Repository
{
    public interface IProviderRepository
    {
        Task<int> GetProviderId(string providerName);

        IQueryable<Provider> GetProviders();

        Task<Provider> GetProvider(int? providerId);
    }
}