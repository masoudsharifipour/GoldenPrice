using GoldenPrice.Application.Model;
using GoldenPrice.Provider;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Provider.brsapi.ir;

public class BRSMarketDataProviderService : IMarketDataProvider
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public BRSMarketDataProviderService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        var brsSettings = _configuration.GetSection("BrsApiSettings");
        _httpClient.BaseAddress = new Uri(brsSettings["BaseUrl"] ?? string.Empty);
    }

    public async Task<MarketModel?> GetAllDataAsync()
    {
        var brsSettings = _configuration.GetSection("BrsApiSettings");
        var apiPath = brsSettings["ApiPath"];
        var apiKey = brsSettings["ApiKey"];
        
        var response = await _httpClient.GetAsync($"{apiPath}?key={apiKey}");

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