using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Client.HttpRepository
{
    public interface ICompanyHttpRepository
    {
        Task<List<CompanyReadDto>> GetCompanies();
    }
}