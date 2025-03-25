using GoldenPrice.Provider;
using GoldenPrice.Provider.brsapi.ir.Model;

namespace Infrastructure;

public class CachedMarketDataProvider : IMarketDataProvider
{
    private readonly IMarketDataProvider _innerProvider;
    private readonly ICacheService _cacheService;
    private readonly TimeSpan _cacheTtl = TimeSpan.FromMinutes(30);

    public CachedMarketDataProvider(IMarketDataProviderFactory providerFactory, ICacheService cacheService, string? providerName)
    {
        _innerProvider = providerFactory.GetProvider(providerName);
        _cacheService = cacheService;
    }

    public async Task<MarketModel?> GetAllDataAsync()
    {
        const string cacheKey = "AllMarketData";
        var cachedData = await _cacheService.GetAsync<MarketModel>(cacheKey);

        if (cachedData != null)
            return cachedData;

        var data = await _innerProvider.GetAllDataAsync();
        await _cacheService.SetAsync(cacheKey, data, _cacheTtl);
        return data;
    }

    public async Task<List<GoldModel>?> GetGoldDataAsync() => (await GetAllDataAsync())?.Gold;

    public async Task<List<CurrencyModel>?> GetCurrencyDataAsync() => (await GetAllDataAsync())?.Currency;

    public async Task<List<CryptocurrencyModel>?> GetCryptocurrencyDataAsync() => (await GetAllDataAsync())?.Cryptocurrency;
}