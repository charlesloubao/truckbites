using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eleven95.TruckBites.Data.Models;

public class Payout
{
    public long PayoutId { get; set; }
    [Required] public required decimal Amount { get; set; }
    [Required] public required DateTime CreatedDate { get; set; }
    [Required] public required DateTime UpdatedDate { get; set; }
    public DateTime? CompletedDate { get; set; }
    [Required] public required string ExternalId { get; set; }

    [Required] public required long FoodTruckId { get; set; }

    [Column(TypeName = "text")] [Required] public required PayoutStatus Status { get; set; }
    [Column(TypeName = "text")] [Required] public required PaymentProcessorType PaymentProcessor { get; set; }
}

public enum PayoutStatus
{
    Pending,
    Completed,
    Cancelled,
    Failed
}