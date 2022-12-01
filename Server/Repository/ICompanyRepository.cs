using Entities.Models;

namespace Server.Repository
{
    public interface ICompanyRepository
    {
        Task<List<Company>> GetCompanies();
        Task CreateCompany(Company company);
        Task<Company> GetCompany(Guid id);
    }
}