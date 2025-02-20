using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorCookingAgenda.Client;

namespace MyBlazorCookingAgenda.Client;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebAssemblyHostBuilder.CreateDefault(args);
        builder.RootComponents.Add<App>("#app");
        builder.RootComponents.Add<HeadOutlet>("head::after");

        builder.Services.AddHttpClient("MyBlazorCookingAgenda.ServerAPI",
                client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

        // Supply HttpClient instances that include access tokens when making requests to the server project
        builder.Services.AddScoped(sp =>
            sp.GetRequiredService<IHttpClientFactory>().CreateClient("MyBlazorCookingAgenda.ServerAPI"));

        builder.Services.AddApiAuthorization();

        await builder.Build().RunAsync();
    }
}