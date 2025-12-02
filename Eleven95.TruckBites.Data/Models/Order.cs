using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eleven95.TruckBites.Data.Models;

public class Order
{
    public long OrderId { get; set; }

    [Required] public required string UserId { get; set; }
    public User User { get; set; }

    [Required] public required long FoodTruckId { get; set; }
    public FoodTruck FoodTruck { get; set; }
    [Required] public required decimal Amount { get; set; }

    [Column(TypeName = "text")] [Required] public required OrderStatus Status { get; set; }
    [Required] public required DateTime CreatedAt { get; set; }
    [Required] public required DateTime UpdatedAt { get; set; }
    
    public long? PaymentId { get; set; }
    public Payment? Payment { get; set; }

    public List<OrderItem> OrderItems { get; set; }
}

public enum OrderStatus
{
    Created,
    PendingPayment,
    PaymentFailed,
    PaymentSucceeded,
    PendingConfirmation,
    Confirmed,
    Processing,
    Completed,
    Cancelled,
    Failed
}