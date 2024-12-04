using Core.Entities;
using Infrastructure.ApiClients;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class CurrencyExchangeService: ICurrencyExchangeService
    {
        private readonly ICurrencyExchangeApiClient _apiClient;
        private readonly ILogger<CurrencyExchangeService> _logger;

        public CurrencyExchangeService(ICurrencyExchangeApiClient apiClient, ILogger<CurrencyExchangeService> logger)
        {
            _apiClient = apiClient;
            _logger = logger;
        }

        public async Task<CurrencyExchangeRate> GetExchangeRatesAsync()
        {
            return await _apiClient.FetchRatesAsync();
        }
    }
}
