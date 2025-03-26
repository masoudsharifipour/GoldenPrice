using Newtonsoft.Json;

namespace GoldenPrice.Application.Model;

public class CryptocurrencyModel
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("time")] public string Time { get; set; }

    [JsonProperty("price")] public decimal Price { get; set; }

    [JsonProperty("change_percent")] public decimal ChangePercent { get; set; }
}