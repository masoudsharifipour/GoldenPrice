using GoldenPrice.Application.Model;
using GoldenPrice.Provider;

namespace GoldenPrice;

public interface IMarketDataService
{
    Task<List<GoldModel>> GetGoldDataAsync();
    Task<List<CurrencyModel>> GetCurrencyDataAsync();
    Task<List<CryptocurrencyModel>> GetCryptocurrencyDataAsync();
    Task<MarketModel> GetAllDataAsync();
}