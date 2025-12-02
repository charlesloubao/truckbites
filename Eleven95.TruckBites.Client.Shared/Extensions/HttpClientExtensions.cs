using Microsoft.Extensions.DependencyInjection;

namespace Eleven95.TruckBites.Client.Shared.Extensions;

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