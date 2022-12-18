
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Data.Configuration;

namespace Reg.Server.Context
{
    public class RegContext : IdentityDbContext<User>
    {
        public RegContext(DbContextOptions<RegContext> options)
            :base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }


        public DbSet<Provider> Providers { get; set; } = null!;
        public DbSet<Region> Regions { get; set; } = null!;
        public DbSet<RequestAbonent> Requests { get; set; } = null!;
        public DbSet<PersonRequestInfo> Persons { get; set; } = null!;
        public DbSet<Company> Companies { get; set; } = null!;
        public DbSet<Leader> Leaders { get; set; } = null!;
        public DbSet<Country> Countries { get; set; } = null!;
        public DbSet<RequestFile> Files { get; set; } = null!;
    }
}
