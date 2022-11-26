
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Reg.Server.Context
{
    public class RegContext : DbContext
    {
        public RegContext(DbContextOptions<RegContext> options)
            :base(options)
        {
        }


        public DbSet<Person> Persons { get; set; }
        public DbSet<Provider> Providers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<RequestAbonent> Requests { get; set; }

    }
}
