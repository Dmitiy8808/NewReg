using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Reg.Server.Context;

namespace Server.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly RegContext _context;
        public CountryRepository(RegContext context)
        {
            _context = context;
            
        }
        public async Task CreateCountries(List<Country> country)
        {
             if(country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }
            await _context.Countries.AddRangeAsync(country);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCountry(Country country)
        {
            if(country == null)
            {
                throw new ArgumentNullException(nameof(country));
            }
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Country>> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country> GetCountry(int id)
        {
            return await _context.Countries.FindAsync(id);
        }
    }
}