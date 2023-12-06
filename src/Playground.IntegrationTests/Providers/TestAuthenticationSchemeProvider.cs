using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Playground.Api.Security;
using Playground.IntegrationTests.Handlers;

namespace Playground.IntegrationTests.Providers;

public class TestAuthenticationSchemeProvider : AuthenticationSchemeProvider
{
    public TestAuthenticationSchemeProvider(IOptions<AuthenticationOptions> options) : base(options)
    {
    }

    protected TestAuthenticationSchemeProvider(IOptions<AuthenticationOptions> options, IDictionary<string, AuthenticationScheme> schemes) : base(options, schemes)
    {
    }

    public override Task<AuthenticationScheme?> GetSchemeAsync(string name)
    {
        if (!PlaygroundAuthenticationScheme.Auth0.Equals(name, StringComparison.Ordinal)) return base.GetSchemeAsync(name);
        var scheme = new AuthenticationScheme(PlaygroundAuthenticationScheme.Auth0,
            PlaygroundAuthenticationScheme.Auth0, typeof(TestAuthenticationHandler));
        
        return Task.FromResult<AuthenticationScheme?>(scheme);
    }
}