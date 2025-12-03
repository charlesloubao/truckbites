using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Data.Services;
using Eleven95.TruckBites.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Eleven95.TruckBites.Vendor.WebApi.Services;

public class OrderFulfillmentService : IOrderFulfillmentService
{
    private readonly ILogger<OrderFulfillmentService> _logger;
    private readonly AppDbContext _dbContext;
    private readonly IUserProvider _userProvider;

    public OrderFulfillmentService(ILogger<OrderFulfillmentService> logger, AppDbContext dbContext,
        IUserProvider userProvider)
    {
        _logger = logger;
        _dbContext = dbContext;
        _userProvider = userProvider;
    }

    public async Task<List<Order>> GetOrdersForFoodTruck(long foodTruckId)
    {
        var orders = await _dbContext.Orders
            .IgnoreQueryFilters()
            .Where(o => o.FoodTruckId == foodTruckId)
            .ToListAsync();

        return orders;
    }

    public async Task<Order?> GetOrderByIdAsync(long foodtruckId, long orderId)
    {
        var order = await _dbContext.Orders
            .IgnoreQueryFilters()
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.OrderId == orderId && o.FoodTruckId == foodtruckId);

        return order;
    }

    public async Task<Order> ConfirmOrderAsync(long foodTruckId, long orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o =>
            o.FoodTruckId == foodTruckId && o.OrderId == orderId);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        order.Status = OrderStatus.Processing;
        order.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> CancelOrderAsync(long foodTruckId, long orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o =>
            o.FoodTruckId == foodTruckId && o.OrderId == orderId);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        order.Status = OrderStatus.Cancelled;
        order.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return order;
    }

    public async Task<Order> CompleteOrderAsync(long foodTruckId, long orderId)
    {
        var order = await _dbContext.Orders.FirstOrDefaultAsync(o =>
            o.FoodTruckId == foodTruckId && o.OrderId == orderId);

        if (order == null)
        {
            throw new Exception("Order not found.");
        }

        order.Status = OrderStatus.Completed;
        order.UpdatedAt = DateTime.UtcNow;

        await _dbContext.SaveChangesAsync();
        return order;
    }
}