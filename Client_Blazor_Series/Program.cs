
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
    BaseAddress = new Uri("https://localhost:7024/")
});


builder.Services.AddScoped<ISerieService, SerieService>();


builder.Services.AddScoped<SeriesViewModel>();
builder.Services.AddScoped<SerieDetailViewModel>();

await builder.Build().RunAsync();
