using System.Security.Claims;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApp.Client.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Eleven95.TruckBites.WebApp.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateApiClient();
    }

    public async Task<SignInResponse> SignInAsync()
    {
        var response = await _httpClient.PostAsync("api/auth/signin", null);
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
        var response = await _httpClient.PostAsync("api/auth/signout", null);
        response.EnsureSuccessStatusCode();
    }
}