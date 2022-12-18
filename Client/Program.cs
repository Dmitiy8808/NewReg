using Blazored.LocalStorage;
using Client.AuthProviders;
using Client.HttpRepository;
using Client.HttpRepository.HttpInterceptor;
using Client.Service;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Reg.Client;
using Toolbelt.Blazor.Extensions.DependencyInjection;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddHttpClient("RequestAPI", (sp, cl) => 
{
    cl.BaseAddress = new Uri("https://localhost:5011/api/");
    cl.EnableIntercept(sp);
});

builder.Services.AddScoped(
    sp => sp.GetService<IHttpClientFactory>().CreateClient("RequestAPI"));

builder.Services.AddHttpClientInterceptor();

builder.Services.AddMudServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IPdfGeneratorHttpRepository, PdfGeneratorHttpRepository>();
builder.Services.AddScoped<IRequestFileHttpRepository, RequestFileHttpRepository>(); 
builder.Services.AddScoped<ICountryHttpRepository, CountryHttpRepository>();  
builder.Services.AddScoped<ICompanyHttpRepository, CompanyHttpRepository>(); 
builder.Services.AddScoped<IRegRequestHttpRepository, RegRequestHttpRepository>(); 
builder.Services.AddScoped<IWebSocketService, WebSocketService>();

builder.Services.AddScoped<HttpInterceptorService>();

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<RefreshTokenService>();

await builder.Build().RunAsync();