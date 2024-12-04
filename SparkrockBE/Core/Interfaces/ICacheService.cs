using Core.Entities;

namespace Core.Interfaces
{
    public interface ICacheService
    {
        Task<CurrencyExchangeRate> GetOrSetLatestExchangeRate(string baseCurrency);
    }
}
