using GoldenPrice.Application.Model;

namespace GoldenPrice.Provider;

public interface IMarketDataProvider
{
    Task<List<GoldModel>?> GetGoldDataAsync();
    Task<List<CurrencyModel>?> GetCurrencyDataAsync();
    Task<List<CryptocurrencyModel>?> GetCryptocurrencyDataAsync();
    Task<MarketModel?> GetAllDataAsync();
}