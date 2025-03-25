using GoldenPrice.Application.GetQuery;
using GoldenPrice.Provider;
using GoldenPrice.Provider.brsapi.ir.Model;
using MediatR;

namespace GoldenPrice;

public class MarketDataService : IMarketDataService
{
    private readonly IMediator _mediator;

    public MarketDataService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<List<GoldModel>> GetGoldDataAsync()
    {
        return await _mediator.Send(new GetMarketDataQuery.GetGoldDataQuery());
    }

    public async Task<List<CurrencyModel>> GetCurrencyDataAsync()
    {
        return await _mediator.Send(new GetMarketDataQuery.GetCurrencyDataQuery());
    }

    public async Task<List<CryptocurrencyModel>> GetCryptocurrencyDataAsync()
    {
        return await _mediator.Send(new GetMarketDataQuery.GetCryptocurrencyDataQuery());
    }

    public async Task<MarketModel> GetAllDataAsync()
    {
        return await _mediator.Send(new GetMarketDataQuery.GetAllDataQuery());
    }
}