using Core.Entities;

namespace Infrastructure.Interfaces
{
    public interface ICurrencyExchangeApiClient
    {
        Task<CurrencyExchangeRate> FetchRatesAsync();
    }
}
