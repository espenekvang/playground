using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Playground.Unleash;
using Unleash;
using Unleash.ClientFactory;

var host = Host.CreateDefaultBuilder().ConfigureServices((context, services) =>
    {
        services.AddSingleton<IUnleash>(_ =>
        {
            var settings = new UnleashSettings()
            {
                AppName = "Playground.Unleash",
                UnleashApi = new Uri("unleash-api-url"),
                CustomHttpHeaders = new Dictionary<string, string>
                {
                    { "Authorization", "unleash-api-key" }
                }
            };

            var unleashFactory = new UnleashClientFactory();

            return unleashFactory.CreateClient(settings, synchronousInitialization: true);
        });
        services.AddTransient<UnleashClient>();
    })
    .Build();

var unleashClient = host.Services.GetRequiredService<UnleashClient>();
Console.WriteLine($"Toggles: {unleashClient.GetFeatureToggleCount()}");
