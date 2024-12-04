using Core.Entities;
using Core.Interfaces;
using Interface.ApiClients;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly ICurrencyExchangeApiClient _currencyExchangeApiClient;
        private readonly IMemoryCache _cache;
        private readonly ILogger<CacheService> _logger;

        public CacheService(ICurrencyExchangeApiClient currencyExchangeApiClient, IMemoryCache cache, ILogger<CacheService> logger)
        {
            _cache = cache;
            _currencyExchangeApiClient = currencyExchangeApiClient;
            _logger = logger;
        }

        public async Task<CurrencyExchangeRate> GetOrSetLatestExchangeRate(string baseCurrency)
        {
            _logger.LogInformation("Fetch data from API for {type}:{currency}", nameof(CurrencyExchangeRate), baseCurrency);

            var rates = _cache.Get<CurrencyExchangeRate>($"{nameof(CurrencyExchangeRate)}:{baseCurrency}");
            if (rates is not null)
            {
                _logger.LogInformation("Data found in cache for {type}:{currency}", nameof(CurrencyExchangeRate), baseCurrency);
                return rates;
            }

            _logger.LogInformation("Data not found in cache for {type}:{currency}, requesting from API", nameof(CurrencyExchangeRate), baseCurrency);
            rates = await _currencyExchangeApiClient.FetchRatesAsync(baseCurrency);
            _cache.Set($"{nameof(CurrencyExchangeRate)}:{baseCurrency}", rates, TimeSpan.FromMinutes(1));

            return rates;
        }
    }
}
