using Core.Entities;
using Refit;

namespace Infrastructure.ApiClients
{
    public interface ICurrencyExchangeApiClient
    {
        [Get("/latest")]
        Task<CurrencyExchangeRate> FetchRatesAsync();
    }
}
