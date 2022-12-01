using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Reg.Server.Context;

namespace Server.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly RegContext _context;
        public CompanyRepository(RegContext context)
        {
            _context = context;
        }

        public async Task<List<Company>> GetCompanies()
        {
            return await _context.Companies
                        .Include(la => la.LocationAddress).ToListAsync();
        }

        public async Task CreateCompany(Company company)
        {
            if(company == null)
            {
                throw new ArgumentNullException(nameof(company));
            }
            _context.Companies.Add(company);
            await _context.SaveChangesAsync();
        }

        public async Task<Company> GetCompany(Guid id)
        {
            return await _context.Companies
                    .Include(la => la.LocationAddress)
                    .FirstOrDefaultAsync(req => req.Id == id);
        }
    }
}