using System.ComponentModel.DataAnnotations;

namespace Eleven95.TruckBites.Data.Models;

public class User
{
    public long UserId { get; set; }
    [Required] public required string EmailAddress { get; set; }
    [Required] public required string DisplayName { get; set; }
    [Required] public DateTime CreatedAt { get; set; }
    [Required] public DateTime UpdatedAt { get; set; }

    public List<Order> Orders { get; set; }
}