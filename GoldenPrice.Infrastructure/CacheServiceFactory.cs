using GoldenPrice.Provider.CacheLayer;
using Infrastructure;
using Infrastructure.CacheLayer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace GoldenPrice.Provider;

public class CacheServiceFactory : ICacheServiceFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly CacheSettings _cacheSettings;

    public CacheServiceFactory(IServiceProvider serviceProvider, IOptions<CacheSettings> cacheSettings)
    {
        _serviceProvider = serviceProvider;
        _cacheSettings = cacheSettings.Value;
    }

    public ICacheService CreateCacheService()
    {
        return _cacheSettings.Type switch
        {
            "InMemory" => _serviceProvider.GetRequiredService<InMemoryCacheService>(),
            "Redis" => _serviceProvider.GetRequiredService<RedisCacheService>(),
            _ => throw new InvalidOperationException("Invalid cache type")
        };
    }
}