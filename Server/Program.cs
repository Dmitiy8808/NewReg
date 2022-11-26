using Reg.Server.Context;
using Reg.Server.MigrationManager;
using Reg.Server.Repository;
using Microsoft.EntityFrameworkCore;
using Server.Repository;
using Server.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Pagination"));
});

builder.Services.AddDbContext<RegContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IProviderRepository, ProviderRepository>(); 
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddTransient<IQualifiedCertificateManager, QualifiedCertificateManager>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.Run();
