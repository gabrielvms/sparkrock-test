using Core.Entities;

namespace Application.Interfaces
{
    public interface ICurrencyExchangeService
    {
        Task<CurrencyExchangeRate> GetExchangeRatesAsync(string baseCurrency);
    }
}
