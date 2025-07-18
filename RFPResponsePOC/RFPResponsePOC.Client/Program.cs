using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using RFPResponsePOC.Model;
using RFPResponsePOC.Models;

namespace RFPResponsePOC.Client;

class Program
{
    static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);

        builder.Services.AddRadzenComponents();

        // Local Storage
        builder.Services.AddBlazoredLocalStorage();

        // Add services to the container.
        AppMetadata appMetadata = new AppMetadata() { Version = "01.00.00" };
        builder.Services.AddSingleton(appMetadata);

        builder.Services.AddScoped<LogService>();
        builder.Services.AddScoped<SettingsService>();
        builder.Services.AddScoped<DatabaseService>();

        // Set the base address for the application
        var http = new HttpClient()
        {
            BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
        };

        builder.Services.AddScoped(sp => http);       

        await builder.Build().RunAsync();
    }
}