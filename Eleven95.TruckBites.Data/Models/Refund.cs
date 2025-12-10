using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eleven95.TruckBites.Data.Models;

public class Refund
{
    public long RefundId { get; set; }
    [Required] public required decimal Amount { get; set; }
    [Required] public required string ExternalId { get; set; }

    [Required] [Column(TypeName = "text")] public PaymentProcessorType PaymentProcessor { get; set; }
    [Required] public required DateTime CreatedAt { get; set; }
    [Required] public required DateTime UpdatedAt { get; set; }

    [Required] public required long OrderId { get; set; }
    public Order Order { get; set; }
    
    [Column(TypeName = "text")] [Required] public required RefundStatus Status { get; set; }
}

public enum RefundStatus
{
    Pending,
    Success,
    Failed
}