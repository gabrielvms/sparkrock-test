using Application.Interfaces;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CurrencyExchangeService: ICurrencyExchangeService
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CurrencyExchangeService> _logger;

        public CurrencyExchangeService(ICacheService cacheService, ILogger<CurrencyExchangeService> logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task<CurrencyExchangeRate> GetExchangeRatesAsync()
        {
            _logger.LogInformation("Get data from ExchangeService for {type}", nameof(CurrencyExchangeRate));

            return await _cacheService.GetOrSetCurrencyExchangeRate();
        }
    }
}
