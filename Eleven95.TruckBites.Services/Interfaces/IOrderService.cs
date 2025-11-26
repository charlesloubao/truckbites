using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.Services.Interfaces;

public interface IOrderService
{
    public Task<List<Order>> GetAllOrdersAsync();
    public Task<Order?> GetOrderByIdAsync(long id);
    public Task<Order> CreateOrderAsync(CreateOrderRequest request);
    public Task<ProcessPaymentResponse> ProcessOrderAsync(Order order);
}