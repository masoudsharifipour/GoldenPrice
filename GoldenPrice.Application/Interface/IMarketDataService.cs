using GoldenPrice.Provider;
using GoldenPrice.Provider.brsapi.ir.Model;

namespace GoldenPrice;

public interface IMarketDataService
{
    Task<List<GoldModel>> GetGoldDataAsync();
    Task<List<CurrencyModel>> GetCurrencyDataAsync();
    Task<List<CryptocurrencyModel>> GetCryptocurrencyDataAsync();
    Task<MarketModel> GetAllDataAsync();
}