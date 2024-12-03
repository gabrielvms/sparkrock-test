using Core.Entities;
using Refit;

namespace Infrastructure.Interfaces
{
    public interface ICurrencyExchangeApiClient
    {
        [Get("/latest")]
        Task<CurrencyExchangeRate> FetchRatesAsync();
    }
}
