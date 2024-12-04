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

        public async Task<CurrencyExchangeRate> GetOrSetCurrencyExchangeRate()
        {
            _logger.LogInformation("Fetch data from API for {type}", nameof(CurrencyExchangeRate));

            var rates = _cache.Get<CurrencyExchangeRate>(nameof(CurrencyExchangeRate));
            if (rates is not null)
            {
                _logger.LogInformation("Data found in cache for {type}", nameof(CurrencyExchangeRate));
                return rates;
            }

            _logger.LogInformation("Data not found in cache for {type}, requesting from API", nameof(CurrencyExchangeRate));
            rates = await _currencyExchangeApiClient.FetchRatesAsync();
            _cache.Set(nameof(CurrencyExchangeRate), rates, TimeSpan.FromMinutes(1));

            return rates;
        }
    }
}
