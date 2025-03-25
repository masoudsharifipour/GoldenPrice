using GoldenPrice.Provider.brsapi.ir.Model;
using Newtonsoft.Json;

namespace GoldenPrice.Provider;

public class MarketModel
{
    [JsonProperty("gold")]
    public List<GoldModel> Gold { get; set; }

    [JsonProperty("currency")]
    public List<CurrencyModel> Currency { get; set; }

    [JsonProperty("cryptocurrency")]
    public List<CryptocurrencyModel> Cryptocurrency { get; set; }
}