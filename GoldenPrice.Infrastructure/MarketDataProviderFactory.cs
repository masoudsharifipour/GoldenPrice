using GoldenPrice.Application;
using GoldenPrice.Provider;
using Infrastructure.Provider.brsapi.ir;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public class MarketDataProviderFactory : IMarketDataProviderFactory
{
    private readonly IServiceProvider _serviceProvider;

    public MarketDataProviderFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IMarketDataProvider GetProvider(string? providerName)
    {
        return providerName switch
        {
            "BRS" => _serviceProvider.GetRequiredService<BRSMarketDataProviderService>(),
            _ => throw new ArgumentException($"Provider '{providerName}' is not supported.")
        };
    }
}
