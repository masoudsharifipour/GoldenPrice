using Newtonsoft.Json;

namespace GoldenPrice.Provider.brsapi.ir.Model;

public class CryptocurrencyModel
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("time")] public string Time { get; set; }

    [JsonProperty("price")] public decimal Price { get; set; }

    [JsonProperty("change_percent")] public decimal ChangePercent { get; set; }
}