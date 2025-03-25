namespace GoldenPrice.Provider;

public interface IMarketDataProviderFactory
{
    IMarketDataProvider GetProvider(string? providerName);
}