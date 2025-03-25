using GoldenPrice.Provider;
using GoldenPrice.Provider.brsapi.ir.Model;
using MediatR;

namespace GoldenPrice.Application.GetQuery;

public class GetMarketDataQuery
{
    public record GetGoldDataQuery : IRequest<List<GoldModel>>;
    public record GetCurrencyDataQuery : IRequest<List<CurrencyModel>>;
    public record GetCryptocurrencyDataQuery : IRequest<List<CryptocurrencyModel>>;
    public record GetAllDataQuery : IRequest<MarketModel>;
    
    
}