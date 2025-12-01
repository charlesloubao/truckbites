using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace Eleven95.TruckBites.WebApp.Client.Extensions;

public static class HttpClientExtensions
{
    public static IHttpClientBuilder AddAppHttpClients(this IServiceCollection services, string baseAddress)
    {
        Action<HttpClient> createApiHttpClient =
            client => { client.BaseAddress = new Uri(baseAddress); };

        return services.AddHttpClient("api", createApiHttpClient);
    }
    
    public static HttpClient CreateApiClient(this IHttpClientFactory clientFactory) => clientFactory.CreateClient("api");
}