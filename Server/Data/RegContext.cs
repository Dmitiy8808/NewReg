
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


        public DbSet<Provider> Providers { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<RequestAbonent> Requests { get; set; } = null!;
        public DbSet<PersonRequestInfo> Persons { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
    }
}
