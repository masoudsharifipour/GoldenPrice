using Newtonsoft.Json;

namespace GoldenPrice.Application.Model;

public class GoldModel
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("time")] public string Time { get; set; }

    [JsonProperty("price")] public decimal Price { get; set; }

    [JsonProperty("change_percent")] public decimal ChangePercent { get; set; }

    [JsonProperty("unit")] public string Unit { get; set; }
}