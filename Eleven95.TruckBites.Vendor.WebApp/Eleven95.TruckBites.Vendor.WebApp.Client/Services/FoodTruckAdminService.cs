using System.Net;
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

    public async Task<FoodTruck?> GetFoodTruckByIdAsync(long foodTruckId)
    {
        var response = await _httpClient.GetAsync($"api/foodtrucks/{foodTruckId}");

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<FoodTruck>();
    }

    public async Task<List<FoodTruckMenuItem>> UpdateFoodTruckMenuAsync(long foodTruckFoodTruckId,
        List<FoodTruckMenuItem> foodTruckMenuItems)
    {
        var response =
            await _httpClient.PutAsJsonAsync($"api/foodtrucks/{foodTruckFoodTruckId}/menu", foodTruckMenuItems);

        response.EnsureSuccessStatusCode();

        return (await response.Content.ReadFromJsonAsync<List<FoodTruckMenuItem>>())!;
    }
}