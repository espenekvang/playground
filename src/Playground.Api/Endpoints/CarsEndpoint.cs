using System.Security.Claims;
using Playground.Api.Models;
using Playground.Api.Security;

namespace Playground.Api.Endpoints;

public static class CarsEndpoint
{
    public static void AddCarsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/api/cars", (ClaimsPrincipal claimsPrincipal) =>
            {
                var onbehalfOfClaim =
                    claimsPrincipal.Claims.FirstOrDefault(claim =>
                        claim.Type == PlaygroundClaimTypes.OnBehalfOfClaimType);

                //Here you can check the value from the onbehalfof claim.
                
                return Results.Ok(new List<Car>
                {
                    new("Polestar", Guid.NewGuid().ToString()),
                    new("VW", Guid.NewGuid().ToString())
                });
            })
            .RequireAuthorization("onbehalfof")
            .WithName("GetCars")
            .WithOpenApi();
    }
}