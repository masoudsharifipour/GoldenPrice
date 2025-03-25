using Microsoft.AspNetCore.Mvc;

namespace GoldenPrice.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MarketDataController : ControllerBase
{
    private readonly IMarketDataService _marketDataService;

    public MarketDataController(IMarketDataService marketDataService)
    {
        _marketDataService = marketDataService;
    }

    [HttpGet("gold")]
    public async Task<IActionResult> GetGoldData()
    {
        var goldData = await _marketDataService.GetGoldDataAsync();
        return Ok(goldData);
    }

    [HttpGet("currency")]
    public async Task<IActionResult> GetCurrencyData()
    {
        var currencyData = await _marketDataService.GetCurrencyDataAsync();
        return Ok(currencyData);
    }

    [HttpGet("cryptocurrency")]
    public async Task<IActionResult> GetCryptocurrencyData()
    {
        var cryptoData = await _marketDataService.GetCryptocurrencyDataAsync();
        return Ok(cryptoData);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllData()
    {
        var allData = await _marketDataService.GetAllDataAsync();
        return Ok(allData);
    }
}