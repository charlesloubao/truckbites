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
            .Where(o => o.FoodTruckId == foodTruckId)
            .ToListAsync();

        return orders;
    }

    public async Task<Order?> GetOrderByIdAsync(long foodtruckId, long orderId)
    {
        var order = await _dbContext.Orders
            .Include(o => o.OrderItems)
            .Include(o => o.Refund)
            .FirstOrDefaultAsync(o => o.OrderId == orderId && o.FoodTruckId == foodtruckId);

        return order;
    }

    public async Task<Order> ConfirmOrderAsync(long foodTruckId, long orderId)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
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
            await transaction.CommitAsync();

            return order;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order> CancelOrderAsync(long foodTruckId, long orderId)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();

        try
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o =>
                o.FoodTruckId == foodTruckId && o.OrderId == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            // Cancel order
            order.Status = OrderStatus.Cancelled;
            order.UpdatedAt = DateTime.UtcNow;

            // Refund user
            var refund = new Refund()
            {
                Amount = order.Amount,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                ExternalId = "",
                OrderId = orderId,
                PaymentProcessor = PaymentProcessorType.None,
                Status = RefundStatus.Success
            };

            await _dbContext.Refunds.AddAsync(refund);
            await _dbContext.SaveChangesAsync();
            await transaction.CommitAsync();
            return order;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<Order> CompleteOrderAsync(long foodTruckId, long orderId)
    {
        var transaction = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(o =>
                o.FoodTruckId == foodTruckId && o.OrderId == orderId);

            if (order == null)
            {
                throw new Exception("Order not found.");
            }

            order.Status = OrderStatus.Completed;
            order.UpdatedAt = DateTime.UtcNow;

            //TODO: This most likely needs to be moved to a background job
            _logger.LogInformation("Creating instant payout to food truck {FoodTruckId}", order.FoodTruckId);

            var payout = new Payout()
            {
                Amount = order.Amount,
                ExternalId = Guid.NewGuid().ToString(),
                PaymentProcessor = PaymentProcessorType.None,
                FoodTruckId = order.FoodTruckId,
                Status = PayoutStatus.Completed,
                CreatedDate = DateTime.UtcNow,
                CompletedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            _dbContext.Payouts.Add(payout);
            await _dbContext.SaveChangesAsync();

            await transaction.CommitAsync();
            return order;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}