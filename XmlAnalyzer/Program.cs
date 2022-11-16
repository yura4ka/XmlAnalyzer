using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using XmlAnalyzer;
using XmlAnalyzer.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<Analyzer>();

await builder.Build().RunAsync();
