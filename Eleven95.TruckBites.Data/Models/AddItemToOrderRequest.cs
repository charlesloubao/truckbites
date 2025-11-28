using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class AddItemToOrderRequest
{
    [Required] public required long OrderId { get; set; }
    [Required] public required long FoodTruckMenuItemId { get; set; }
    [Required] public required long FoodTruckId { get; set; }
    [Required] public required int Quantity { get; set; }
}