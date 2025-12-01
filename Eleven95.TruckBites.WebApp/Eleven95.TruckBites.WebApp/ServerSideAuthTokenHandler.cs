using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authentication;

namespace Eleven95.TruckBites.WebApp;

public class ServerSideAuthTokenHandler(IHttpContextAccessor httpContextAccessor) : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var httpContext = httpContextAccessor.HttpContext;

        if (httpContext != null)
        {
            var token = await httpContext.GetTokenAsync("access_token");

            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
