using GoldenPrice.Application.GetQuery;
using GoldenPrice.Application.Model;
using GoldenPrice.Provider;
using MediatR;

namespace GoldenPrice.Application.Handler;



public class GetGoldDataHandler : IRequestHandler<GetMarketDataQuery.GetGoldDataQuery, List<GoldModel>>
{
    private readonly IMarketDataProvider _marketDataProvider;

    public GetGoldDataHandler(IMarketDataProvider marketDataProvider)
    {
        _marketDataProvider = marketDataProvider;
    }

    public async Task<List<GoldModel>?> Handle(GetMarketDataQuery.GetGoldDataQuery request, CancellationToken cancellationToken)
    {
        return await _marketDataProvider.GetGoldDataAsync();
    }
}

public class GetCurrencyDataHandler : IRequestHandler<GetMarketDataQuery.GetCurrencyDataQuery, List<CurrencyModel>>
{
    private readonly IMarketDataProvider _marketDataProvider;

    public GetCurrencyDataHandler(IMarketDataProvider marketDataProvider)
    {
        _marketDataProvider = marketDataProvider;
    }

    public async Task<List<CurrencyModel>?> Handle(GetMarketDataQuery.GetCurrencyDataQuery request, CancellationToken cancellationToken)
    {
        return await _marketDataProvider.GetCurrencyDataAsync();
    }
}

public class GetCryptocurrencyDataHandler : IRequestHandler<GetMarketDataQuery.GetCryptocurrencyDataQuery, List<CryptocurrencyModel>>
{
    private readonly IMarketDataProvider _marketDataProvider;

    public GetCryptocurrencyDataHandler(IMarketDataProvider marketDataProvider)
    {
        _marketDataProvider = marketDataProvider;
    }

    public async Task<List<CryptocurrencyModel>?> Handle(GetMarketDataQuery.GetCryptocurrencyDataQuery request, CancellationToken cancellationToken)
    {
        return await _marketDataProvider.GetCryptocurrencyDataAsync();
    }
}

public class GetAllDataHandler : IRequestHandler<GetMarketDataQuery.GetAllDataQuery, MarketModel>
{
    private readonly IMarketDataProvider _marketDataProvider;

    public GetAllDataHandler(IMarketDataProvider marketDataProvider)
    {
        _marketDataProvider = marketDataProvider;
    }

    public async Task<MarketModel?> Handle(GetMarketDataQuery.GetAllDataQuery request, CancellationToken cancellationToken)
    {
        return await _marketDataProvider.GetAllDataAsync();
    }
}

