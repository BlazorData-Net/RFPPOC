using Blazored.LocalStorage;
using Radzen;
using RFPResponsePOC.Client.Pages;
using RFPResponsePOC.Components;
using RFPResponsePOC.Model;
using RFPResponsePOC.Models;

namespace RFPResponsePOC;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        // Add services to the container.
        builder.Services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();

        builder.Services.AddRadzenComponents();

        // Local Storage
        builder.Services.AddBlazoredLocalStorage();

        // Add services to the container.
        AppMetadata appMetadata = new AppMetadata() { Version = "01.00.00" };
        builder.Services.AddSingleton(appMetadata);

        builder.Services.AddScoped<LogService>();
        builder.Services.AddScoped<SettingsService>();
        builder.Services.AddScoped<DatabaseService>();

        // Register HttpClient
        builder.Services.AddHttpClient();
        builder.Services.AddScoped<HttpClient>();

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseWebAssemblyDebugging();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();

        app.UseAntiforgery();

        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(Client._Imports).Assembly);

        app.Run();
    }
}
