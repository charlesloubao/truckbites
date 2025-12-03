using System.Net.Http.Headers;
using Auth0.AspNetCore.Authentication;
using Eleven95.TruckBites.Client.Shared.Extensions;
using Eleven95.TruckBites.Server.Shared;
using Eleven95.TruckBites.Services.Interfaces;
using Eleven95.TruckBites.Vendor.WebApp.Client.Services;
using Eleven95.TruckBites.Vendor.WebApp.Components;
using Microsoft.AspNetCore.Authentication;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"]!;
    options.ClientId = builder.Configuration["Auth0:ClientId"]!;
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"]!;
}).WithAccessToken(options => { options.Audience = builder.Configuration["Auth0:Audience"]; });

builder.Services.AddAppHttpClients(builder.Configuration["Api:BaseUrl"]!)
    .AddHttpMessageHandler<ServerSideAuthTokenHandler>();

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

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents()
    .AddAuthenticationStateSerialization(options => options.SerializeAllClaims = true);

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<IFoodTruckAdminService, FoodTruckAdminService>();
builder.Services.AddScoped<IOrderFulfillmentService, OrderFulfillmentService>();

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

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseAntiforgery();

app.MapControllers();
app.MapReverseProxy();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Eleven95.TruckBites.Vendor.WebApp.Client._Imports).Assembly);

app.Run();