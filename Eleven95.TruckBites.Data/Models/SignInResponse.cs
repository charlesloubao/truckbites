namespace Eleven95.TruckBites.Data.Models;

public class SignInResponse : APIResponse
{
    public User? User { get; set; }
    public string? Token { get; set; }
}