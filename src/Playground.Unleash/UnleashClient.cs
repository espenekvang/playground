using Microsoft.Extensions.Logging;
using Unleash;

namespace Playground.Unleash;

internal class UnleashClient
{
    private readonly ILogger<UnleashClient> _logger;
    private readonly IUnleash _unleash;

    public UnleashClient(ILogger<UnleashClient> logger, IUnleash unleash)
    {
        _logger = logger;
        _unleash = unleash;
    }

    public int GetFeatureToggleCount()
    {
        return _unleash.FeatureToggles.Count;
    }
}