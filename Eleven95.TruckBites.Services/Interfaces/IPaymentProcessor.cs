using Eleven95.TruckBites.Data.Models;

namespace Eleven95.TruckBites.Services.Interfaces;

public interface IPaymentProcessor
{
    public Task<ProcessPaymentResponse> ProcessPaymentAsync(ProcessPaymentRequest request);
}