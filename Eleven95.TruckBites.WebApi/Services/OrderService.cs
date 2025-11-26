using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.WebApi.Services;

public class OrderService : IOrderService
{
    private readonly ILogger<OrderService> _logger;
    private readonly IPaymentProcessor _paymentProcessor;
    private readonly AppDbContext _dbContext;

    public OrderService(AppDbContext dbContext, IPaymentProcessor paymentProcessor, ILogger<OrderService> logger)
    {
        _dbContext = dbContext;
        _paymentProcessor = paymentProcessor;
        _logger = logger;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _dbContext.Orders.ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(long id)
    {
        return await _dbContext.Orders
            .Include(o => o.FoodTruck)
            .FirstOrDefaultAsync(o => o.OrderId == id);
    }

    public async Task<Order> CreateOrderAsync(CreateOrderRequest request)
    {
        _logger.LogInformation($"Creating order for food truck {request.FoodTruckId}");
        var order = new Order()
        {
            Amount = 0,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            FoodTruckId = request.FoodTruckId,
            Status = OrderStatus.Created
        };

        _dbContext.Orders.Add(order);
        await _dbContext.SaveChangesAsync();

        return order;
    }

    public async Task<ProcessPaymentResponse> ProcessOrderAsync(Order order)
    {
        _logger.LogInformation("Processing order {OrderOrderId}", order.OrderId);
        order.Status = OrderStatus.Completed;
        order.UpdatedAt = DateTime.UtcNow;

        _dbContext.Orders.Update(order);
        await _dbContext.SaveChangesAsync();

        return new ProcessPaymentResponse(true, null);
    }
}