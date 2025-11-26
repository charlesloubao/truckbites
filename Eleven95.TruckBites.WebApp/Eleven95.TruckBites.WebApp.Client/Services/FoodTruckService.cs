using System.Net.Http.Json;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApp.Client.Extensions;

namespace Eleven95.TruckBites.WebApp.Client.Services;

public class FoodTruckService : IFoodTruckService
{
    private readonly HttpClient _httpClient;

    public FoodTruckService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateApiClient();
    }

    public async Task<List<FoodTruck>> GetAllFoodTrucksAsync()
    {
        var response = await _httpClient.GetAsync("api/FoodTrucks");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<List<FoodTruck>>())!;
    }

    public async Task<FoodTruck?> GetFoodTruckByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"api/FoodTrucks/{id}");

        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<FoodTruck>();
    }
}