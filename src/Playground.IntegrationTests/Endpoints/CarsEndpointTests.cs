using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Playground.IntegrationTests.Providers;

namespace Playground.IntegrationTests.Endpoints;

public class CarsEndpointTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Cars_AuthenticationSet_ReturnsCars()
    {
        //arrange
        var httpClient = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
                services.AddTransient<IAuthenticationSchemeProvider, TestAuthenticationSchemeProvider>();
            }))
            .CreateDefaultClient();
        
        //act
        var response = await httpClient.GetAsync(new Uri("/api/cars", UriKind.Relative));

        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
    
    [Fact]
    public async Task Cars_AuthenticationMissing_NoCarsReturned()
    {
        //arrange
        var httpClient = factory.WithWebHostBuilder(builder => builder.ConfigureTestServices(services =>
            {
            }))
            .CreateDefaultClient();
        
        //act
        var response = await httpClient.GetAsync(new Uri("/api/cars", UriKind.Relative));

        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }
}