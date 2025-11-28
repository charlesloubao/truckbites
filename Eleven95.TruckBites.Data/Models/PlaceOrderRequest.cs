using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class PlaceOrderRequest
{
    [Required] public required long OrderId { get; set; }
}