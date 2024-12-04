using Core.Entities;
using Refit;

namespace Infrastructure.ApiClients
{
    public interface ICurrencyExchangeApiClient
    {
        [Get("/latests")]
        Task<CurrencyExchangeRate> FetchRatesAsync();
    }
}
