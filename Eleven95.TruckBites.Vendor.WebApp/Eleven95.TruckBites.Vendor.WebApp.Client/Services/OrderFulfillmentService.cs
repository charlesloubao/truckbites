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

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        var response = await _httpClient.GetAsync("api/orders");
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<List<Order>>())!;
    }

    public async Task<Order?> GetOrderByIdAsync(long id)
    {
        var response = await _httpClient.GetAsync($"api/orders/{id}");

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<Order>();
    }

    public Task<Order> ConfirmOrderAsync(long orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> CancelOrderAsync(long orderId)
    {
        throw new NotImplementedException();
    }

    public Task<Order> CompleteOrderAsync(long orderId)
    {
        throw new NotImplementedException();
    }
}