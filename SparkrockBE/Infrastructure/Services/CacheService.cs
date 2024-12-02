using Infrastructure.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _cache;
        private readonly HttpClient _httpClient;
        private readonly ILogger<CacheService> _logger;

        public CacheService()
        {
        }
        public Task<T> GetOrSetAsync<T>(string key, Func<CancellationToken, Task<T>> getItemCallback, int expirationInSeconds = 10, CancellationToken cancellationToken = default)
        {
            
        }
    }
}
