using System.Diagnostics;
using System.Net.Http.Headers;
using Auth0.AspNetCore.Authentication;
using Eleven95.TruckBites.Client.Shared.Extensions;
using Eleven95.TruckBites.Data;
using Eleven95.TruckBites.Server.Shared;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.WebApp;
using Eleven95.TruckBites.WebApp.Client.Services;
using Eleven95.TruckBites.WebApp.Components;
using Eleven95.TruckBites.WebApp.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.HttpOverrides;
using Yarp.ReverseProxy.Transforms;
using AuthService = Eleven95.TruckBites.WebApp.Services.AuthService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"]!;
    options.ClientId = builder.Configuration["Auth0:ClientId"]!;
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"]!;
}).WithAccessToken(options => { options.Audience = builder.Configuration["Auth0:Audience"]; });

builder.Services.AddAuthorization();

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(builderContext =>
    {
        // This is used during the WebAssembly phase when YARP acts as a reverse proxy

        // We add a transform to attach the JWT before sending to the API.
        builderContext.AddRequestTransform(async transformContext =>
        {
            // Get the current user from the Cookie context
            var user = transformContext.HttpContext.User;

            // IF you are using an external IDP (Auth0, Entra ID, IdentityServer):
            // Retrieve the Access Token saved in the cookie properties
            var token = await transformContext.HttpContext.GetTokenAsync("access_token");

            transformContext.ProxyRequest.Headers.Remove("Cookie");

            if (!string.IsNullOrEmpty(token))
            {
                transformContext.ProxyRequest.Headers.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        });
    });

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);


// This cache is used to persist state across prerendering.
builder.Services.AddSingleton<IMemoryCache, MemoryCache>();

builder.Services.AddScoped<IFoodTruckService, FoodTruckService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPaymentProcessor, PaymentProcessor>();
builder.Services.AddScoped<IAuthService, AuthService>();

// This is used during pre-rendering to pass tokens to the underlining API
builder.Services.AddHttpContextAccessor();
builder.Services.AddTransient<ServerSideAuthTokenHandler>();

builder.Services.AddAppHttpClients(builder.Configuration["Api:BaseUrl"]!)
    .AddHttpMessageHandler<ServerSideAuthTokenHandler>();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });
;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedProto
});

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();
app.MapControllers();
app.MapReverseProxy();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Eleven95.TruckBites.WebApp.Client._Imports).Assembly);

app.Run();