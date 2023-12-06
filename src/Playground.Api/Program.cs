using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Playground.Api.Endpoints;
using Playground.Api.Models;
using Playground.Api.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(config =>
    {
        config.DefaultAuthenticateScheme = PlaygroundAuthenticationScheme.Auth0;
        config.DefaultChallengeScheme = PlaygroundAuthenticationScheme.Auth0;
    })
    .AddJwtBearer(config =>
    {
        config.RequireHttpsMetadata = false;
        config.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = false,
            ValidateAudience = false
        };

    });
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("onbehalfof", policy => policy.RequireClaim(PlaygroundClaimTypes.OnBehalfOfClaimType));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.AddCarsEndpoint();

app.Run();

public partial class Program{}