using System.Net.Http.Json;
using System.Security.Claims;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApp.Client.Extensions;

namespace Eleven95.TruckBites.WebApp.Client.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateApiClient();
    }

    public async Task<SignInResponse> SignInWithEmailAndPassword(SignInRequest model)
    {
        var response = await _httpClient.PostAsync("/app/auth/signin", JsonContent.Create(model));
        try
        {
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<SignInResponse>())!;
        }
        catch (HttpRequestException)
        {
            var content = await response.Content.ReadFromJsonAsync<SignInResponse>();

            if (content != null)
            {
                return content;
            }

            throw;
        }
    }

    public async Task SignOutAsync()
    {
        var response = await _httpClient.PostAsync("/app/auth/signout", null);
        response.EnsureSuccessStatusCode();
    }
}