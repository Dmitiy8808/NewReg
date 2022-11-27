using Client.Service;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Reg.Client;
using Reg.Client.HttpRepository;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Logging.AddConfiguration(builder.Configuration.GetSection("Logging"));

builder.Services.AddHttpClient("ProductsAPI", cl => 
{
    cl.BaseAddress = new Uri("https://localhost:5011/api/");
});

builder.Services.AddScoped(
    sp => sp.GetService<IHttpClientFactory>().CreateClient("ProductsAPI"));

builder.Services.AddMudServices();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IRegRequestHttpRepository, RegRequestHttpRepository>(); 
builder.Services.AddScoped<IWebSocketService, WebSocketService>();

await builder.Build().RunAsync();