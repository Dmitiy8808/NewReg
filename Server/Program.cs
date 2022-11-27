using Reg.Server.Context;
using Reg.Server.MigrationManager;
using Microsoft.EntityFrameworkCore;
using Server.Repository;
using Server.Services;
using NLog;
using Server.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

builder.Services.AddCors(policy =>
{
    policy.AddPolicy("CorsPolicy", opt => opt
    .AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    .WithExposedHeaders("X-Pagination"));
});

builder.Services.AddDbContext<RegContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ConfigureLoggerService();

builder.Services.AddScoped<IProviderRepository, ProviderRepository>(); 
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddTransient<IQualifiedCertificateManager, QualifiedCertificateManager>();  
builder.Services.AddTransient<IRequestRepository, RequestRepository>();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.Run();
