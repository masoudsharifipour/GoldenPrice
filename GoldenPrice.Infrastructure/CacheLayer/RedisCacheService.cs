using GoldenPrice.Provider;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace Infrastructure.CacheLayer;

public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly int _databaseNumber = 1;

    public RedisCacheService(IConnectionMultiplexer connectionMultiplexer, int databaseNumber = 1)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _databaseNumber = databaseNumber;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var db = _connectionMultiplexer.GetDatabase(_databaseNumber);
        var value = await db.StringGetAsync(key);
        return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default;
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan ttl)
    {
        var db = _connectionMultiplexer.GetDatabase(_databaseNumber); 
        var json = JsonConvert.SerializeObject(value);
        await db.StringSetAsync(key, json, ttl);
    }

    public async Task RemoveAsync(string key)
    {
        var db = _connectionMultiplexer.GetDatabase(_databaseNumber); 
        await db.KeyDeleteAsync(key);
    }
}