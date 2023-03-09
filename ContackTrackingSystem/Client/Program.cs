using ContactTrackingSystem.Shared.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using ContactTrackingSystem.Shared.Services;

namespace ContactTrackingSystem.Client
{
    public class Program
    {
        /// <summary>
        /// Web app entry point, adds the required services for the app
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
            builder.Services.AddScoped<ICandidateService, CandidateService>();
            builder.Services.AddScoped(typeof(IDataService<>), typeof(DataService <>));
            builder.Services.AddMemoryCache();
            await builder.Build().RunAsync();
        }
    }
}