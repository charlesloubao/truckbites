using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class CreateOrderRequest
{
    [Required] public required long FoodTruckId { get; set; }
}