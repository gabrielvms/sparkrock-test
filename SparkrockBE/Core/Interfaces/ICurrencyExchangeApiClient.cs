using Core.Entities;
using Refit;

namespace Core.Interfaces
{
    public interface ICurrencyExchangeApiClient
    {
        [Get("/latest")]
        Task<CurrencyExchangeRate> FetchRatesAsync();
    }
}
