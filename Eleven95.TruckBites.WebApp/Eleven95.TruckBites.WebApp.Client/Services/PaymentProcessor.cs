using Eleven95.TruckBites.Data.Models;
using Eleven95.TruckBites.Services.Interfaces;

namespace Eleven95.TruckBites.WebApp.Client.Services;

public class PaymentProcessor:IPaymentProcessor
{
    public Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request)
    {
        throw new NotImplementedException();
    }
}