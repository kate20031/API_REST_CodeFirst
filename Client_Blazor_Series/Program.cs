using Client_Blazor_Series;
using Client_Blazor_Series.Services;
using Client_Blazor_Series.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri("http://localhost:5081/")
});

builder.Services.AddScoped<ISerieService, SerieService>();
builder.Services.AddScoped<SeriesViewModel>();

await builder.Build().RunAsync();
