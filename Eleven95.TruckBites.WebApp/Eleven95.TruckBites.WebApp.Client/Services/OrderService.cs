using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using System.Net.Http.Json;
using Eleven95.TruckBites.WebApp.Client.Extensions;

namespace Eleven95.TruckBites.WebApp.Client.Services;

public class OrderService : IOrderService
{
    private readonly HttpClient _httpClient;

    public OrderService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateApiClient();
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

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync("api/orders", request);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Order>())!;
    }

    public async Task<ProcessPaymentResponse> ProcessOrderAsync(Order order)
    {
        throw new NotImplementedException();
    }

    public async Task<Order> AddItemToOrderAsync(AddItemToOrderRequest request)
    {
        var response = await _httpClient.PostAsJsonAsync($"api/orders/{request.OrderId}/items", request);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<Order>())!;
    }

    public async Task<APIResponse> PlaceOrderAsync(long orderId)
    {
        var response = await _httpClient.PostAsync($"api/orders/{orderId}/place-order", null);
        response.EnsureSuccessStatusCode();
        return (await response.Content.ReadFromJsonAsync<APIResponse>())!;
    }
}