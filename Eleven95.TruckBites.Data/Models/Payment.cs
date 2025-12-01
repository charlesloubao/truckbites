using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eleven95.TruckBites.Data.Models;

public class Payment
{
    public long PaymentId { get; set; }
    [Required] public required decimal Amount { get; set; }

    [Required] public required string ExternalId { get; set; }

    [Required] [Column(TypeName = "text")] public PaymentProcessorType PaymentProcessorType { get; set; }
    [Required] public required DateTime CreatedAt { get; set; }
    [Required] public required DateTime UpdatedAt { get; set; }

    [Column(TypeName = "text")] [Required] public required PaymentStatus Status { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Success,
    Failed
}