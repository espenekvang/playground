using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Playground.Api.Security;

namespace Playground.IntegrationTests.Handlers;

public class TestAuthenticationSchemeOptions : AuthenticationSchemeOptions{}

public class TestAuthenticationHandler : AuthenticationHandler<TestAuthenticationSchemeOptions>
{
    public TestAuthenticationHandler(
        IOptionsMonitor<TestAuthenticationSchemeOptions> options, 
        ILoggerFactory logger,
        UrlEncoder encoder, 
        ISystemClock clock) : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>
        {
            new(PlaygroundClaimTypes.OnBehalfOfClaimType, "an-example-on-behalf-of-claim") //add the claim(s) that are required for the authentication to work
        };
        var identity = new ClaimsIdentity(claims, PlaygroundAuthenticationScheme.Auth0);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, PlaygroundAuthenticationScheme.Auth0);

        var result = AuthenticateResult.Success(ticket);

        return Task.FromResult(result);
    }
}