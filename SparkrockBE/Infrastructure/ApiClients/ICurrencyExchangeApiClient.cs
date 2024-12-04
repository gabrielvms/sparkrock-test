using Core.Entities;
using Refit;

namespace Interface.ApiClients
{
    public interface ICurrencyExchangeApiClient
    {
        [Get("/latest")]
        Task<CurrencyExchangeRate> FetchRatesAsync();
    }
}
