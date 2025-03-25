using GoldenPrice.Provider;
using GoldenPrice.Provider.brsapi.ir.Model;
using Newtonsoft.Json;

namespace GoldenPrice.Application;

public class BRSMarketDataProviderService : IMarketDataProvider
{
    private readonly HttpClient _httpClient;

    public BRSMarketDataProviderService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://brsapi.ir/");
    }

    public async Task<MarketModel?> GetAllDataAsync()
    {
        var response = await _httpClient.GetAsync("/FreeTsetmcBourseApi/Api_Free_Gold_Currency_v2.json");

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine($"Error: {response.StatusCode}");
            var errorContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine(errorContent);
            return null;
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<MarketModel>(responseString);
        return result;
    }

    public async Task<List<GoldModel>?> GetGoldDataAsync()
    {
        var data = await GetAllDataAsync();
        return data?.Gold;
    }

    public async Task<List<CurrencyModel>?> GetCurrencyDataAsync()
    {
        var data = await GetAllDataAsync();
        return data?.Currency;
    }

    public async Task<List<CryptocurrencyModel>?> GetCryptocurrencyDataAsync()
    {
        var data = await GetAllDataAsync();
        return data?.Cryptocurrency;
    }
}