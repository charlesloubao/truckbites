using System.Net.Http.Json;
using Eleven95.TruckBites.Client.Shared.Extensions;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;

namespace Eleven95.TruckBites.Vendor.WebApp.Client.Services;

public class FoodTruckAdminService : IFoodTruckAdminService
{
    private readonly HttpClient _httpClient;

    public FoodTruckAdminService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateApiClient();
    }

    public async Task<List<FoodTruck>?> GetFoodTrucksAsync()
    {
        var response = await _httpClient.GetAsync("api/foodtrucks");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<List<FoodTruck>>();
    }

    public async Task<FoodTruck?> GetFoodTruckByIdAsync(long orderId)
    {
        var response = await _httpClient.GetAsync($"api/foodtrucks/{orderId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<FoodTruck>();
    }
}