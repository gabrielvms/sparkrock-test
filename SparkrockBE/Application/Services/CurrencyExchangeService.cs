using Core.Entities;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CurrencyExchangeService: ICurrencyExchangeService
    {
        private readonly ICurrencyExchangeApiClient _apiClient;
        private readonly ICacheService _cacheService;
        private readonly ILogger<CurrencyExchangeService> _logger;

        public CurrencyExchangeService(ICurrencyExchangeApiClient apiClient, ICacheService cacheService, ILogger<CurrencyExchangeService> logger)
        {
            _apiClient = apiClient;
            _cacheService = cacheService;
            _logger = logger;
        }

        public Task<CurrencyExchangeRate> GetExchangeRatesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
