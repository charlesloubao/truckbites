using System.Net.Http.Json;
using Eleven95.TruckBites.Client.Shared.Extensions;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;

namespace Eleven95.TruckBites.Vendor.WebApp.Client.Services;

public class OrderFulfillmentService : IOrderFulfillmentService
{
    private readonly HttpClient _httpClient;

    public OrderFulfillmentService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateApiClient();
    }

    public async Task<List<Order>> GetOrdersForFoodTruck(long foodTruckId)
    {
        var response = await _httpClient.GetAsync($"api/FoodTrucks/{foodTruckId}/orders");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<List<Order>>())!;
    }

    public async Task<Order?> GetOrderByIdAsync(long foodtruckId, long orderId)
    {
        var response = await _httpClient.GetAsync($"api/FoodTrucks/{foodtruckId}/orders/{orderId}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Order>();
    }

    public async Task<Order> ConfirmOrderAsync(long foodTruckId, long orderId)
    {
        var response = await _httpClient.PostAsync($"api/FoodTrucks/{foodTruckId}/orders/{orderId}/confirm", null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Order>())!;
    }

    public async Task<Order> CancelOrderAsync(long foodTruckId, long orderId)
    {
        var response = await _httpClient.PostAsync($"api/FoodTrucks/{foodTruckId}/orders/{orderId}/cancel", null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Order>())!;
    }

    public async Task<Order> CompleteOrderAsync(long foodTruckId, long orderId)
    {
        var response = await _httpClient.PostAsync($"api/FoodTrucks/{foodTruckId}/orders/{orderId}/complete", null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Order>())!;
    }
}