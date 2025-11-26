using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;

namespace Eleven95.TruckBites.WebApi.Services;

public class StripePaymentProcessor : IPaymentProcessor
{
    public async Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
    {
        await Task.Delay(1000);
        return new ProcessPaymentResponse(true, null);
    }
}