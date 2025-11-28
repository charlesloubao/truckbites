using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class OrderItem
{
    public long OrderItemId { get; set; }

    [Required] public required long OrderId { get; set; }

    [Required] public required long FoodTruckMenuItemId { get; set; }
    public FoodTruckMenuItem FoodTruckMenuItem { get; set; }

    // --- SNAPSHOT DATA ---
    // These values are copied from the MenuItem AT THE MOMENT of purchase.
    // Even if MenuItem.Name changes later, this remains "Cheeseburger".
    public string ItemName { get; set; }

    // Even if MenuItem.Price changes to $15.00 later, this remains $12.50.
    public decimal ItemPrice { get; set; }

    public int Quantity { get; set; }
    
    // Calculated property, not necessarily stored
    public decimal TotalPrice => ItemPrice * Quantity;

    [Required] public required DateTime CreatedAt { get; set; }
    [Required] public required DateTime UpdatedAt { get; set; }
}