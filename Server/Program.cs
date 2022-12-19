using Reg.Server.Context;
using Reg.Server.MigrationManager;
using Microsoft.EntityFrameworkCore;
using Server.Repository;
using Server.Services;
using NLog;
using Server.Extensions;
using DinkToPdf.Contracts;
using DinkToPdf;
using Microsoft.AspNetCore.Identity;
using Server.Services.EmailService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Entities.Configuration;
using Server.Data;

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

builder.Services.AddSingleton(builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());

builder.Services.AddDbContext<RegContext>(opt => opt.UseNpgsql(builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.ConfigureLoggerService();

builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

builder.Services.AddScoped<IFileRepository, FileRepository>(); 
builder.Services.AddScoped<ICountryRepository, CountryRepository>(); 
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>(); 
builder.Services.AddScoped<IProviderRepository, ProviderRepository>(); 
builder.Services.AddScoped<IRegionRepository, RegionRepository>();
builder.Services.AddTransient<IQualifiedCertificateManager, QualifiedCertificateManager>();  
builder.Services.AddTransient<IRequestRepository, RequestRepository>();

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<RegContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt => 
    opt.TokenLifespan = TimeSpan.FromHours(1));

var jwtSettings = builder.Configuration.GetSection("JWTSettings"); 
builder.Services.AddAuthentication(opt => 
{ 
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme; 
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme; 
}).AddJwtBearer(options => 
{ 
    options.TokenValidationParameters = new TokenValidationParameters 
    { 
        ValidateIssuer = true, 
        ValidateAudience = true, 
        ValidateLifetime = true, 
        ValidateIssuerSigningKey = true, 
                    
        ValidIssuer = jwtSettings["validIssuer"], 
        ValidAudience = jwtSettings["validAudience"], 
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"])) 
    }; 
});

builder.Services.Configure<JwtConfiguration>(builder.Configuration.GetSection("JWTSettings"));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddControllers();

var app = builder.Build();



// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase();
app.Run();
