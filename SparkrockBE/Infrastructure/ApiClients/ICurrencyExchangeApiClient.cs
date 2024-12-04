using Core.Entities;
using Refit;

namespace Interface.ApiClients
{
    public interface ICurrencyExchangeApiClient
    {
        [Get("/latest")]
        Task<CurrencyExchangeRate> FetchRatesAsync([Query] string @base, [Query] string currencies = "USD,EUR,GBP,JPY,CNY,BRL,SOL,XRP,BTC,ETH");
    }
}
