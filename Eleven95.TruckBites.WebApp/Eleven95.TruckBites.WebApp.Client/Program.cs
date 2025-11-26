using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApp.Client.Extensions;
using Eleven95.TruckBites.WebApp.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddAuthenticationStateDeserialization();

builder.Services.AddSingleton<IFoodTruckService, FoodTruckService>();
builder.Services.AddSingleton<IOrderService, OrderService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddAppHttpClients(builder.HostEnvironment.BaseAddress);

await builder.Build().RunAsync();