using Core.Entities;
using Core.Interfaces;
using Infrastructure.Services;
using Interface.ApiClients;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Infrastructure.Tests
{
    public class CacheServiceTests
    {
        private readonly Mock<ICurrencyExchangeApiClient> _mockApiClient;
        private readonly Mock<IMemoryCache> _mockMemoryCache;
        private readonly Mock<ILogger<CacheService>> _mockLogger;
        private readonly CacheService _cacheService;

        public CacheServiceTests()
        {
            _mockApiClient = new Mock<ICurrencyExchangeApiClient>();
            _mockMemoryCache = new Mock<IMemoryCache>();
            _mockLogger = new Mock<ILogger<CacheService>>();
            _cacheService = new CacheService(_mockApiClient.Object, _mockMemoryCache.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetOrSetCurrencyExchangeRate_ReturnsCachedRates_WhenCacheIsAvailable()
        {
            // Arrange
            object? cachedRates = new CurrencyExchangeRate
            {
                Date = DateTime.UtcNow,
                Base = "USD",
                Rates = new Dictionary<string, decimal> { { "EUR", 0.85M }, { "JPY", 110.25M } }
            };

            _mockMemoryCache
                .Setup(cache => cache.TryGetValue(It.IsAny<string>(), out cachedRates))
                .Returns(true);

            // Act
            var result = await _cacheService.GetOrSetLatestExchangeRate(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(cachedRates, result);

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Data found in cache")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task GetOrSetCurrencyExchangeRate_FetchesFromApi_WhenCacheIsEmpty()
        {
            // Arrange
            var apiRates = new CurrencyExchangeRate
            {
                Date = DateTime.UtcNow,
                Base = "USD",
                Rates = new Dictionary<string, decimal> { { "EUR", 0.85M }, { "JPY", 110.25M } }
            };

            object? cacheValue = null;
            _mockMemoryCache
                .Setup(cache => cache.TryGetValue(It.IsAny<string>(), out cacheValue))
                .Returns(false);

            _mockApiClient
                .Setup(client => client.FetchRatesAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(apiRates);

            var cacheEntry = Mock.Of<ICacheEntry>();
            cacheEntry.Value = apiRates;

            _mockMemoryCache
                .Setup(cache => cache.CreateEntry(It.IsAny<string>()))
                .Returns(cacheEntry);

            // Act
            var result = await _cacheService.GetOrSetLatestExchangeRate(It.IsAny<string>());

            // Assert
            Assert.NotNull(result);
            Assert.Equal(apiRates, result);

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Data not found in cache")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);

            _mockApiClient.Verify(client => client.FetchRatesAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _mockMemoryCache.Verify(cache => cache.CreateEntry(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task GetOrSetCurrencyExchangeRate_ThrowsException_WhenApiCallFails()
        {
            // Arrange
            object? cacheValue = null;
            _mockMemoryCache
                .Setup(cache => cache.TryGetValue(It.IsAny<string>(), out cacheValue))
                .Returns(false);

            _mockApiClient
                .Setup(client => client.FetchRatesAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ThrowsAsync(new InvalidOperationException("API error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _cacheService.GetOrSetLatestExchangeRate(It.IsAny<string>()));

            Assert.Equal("API error", exception.Message);

            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Data not found in cache")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);

            _mockApiClient.Verify(client => client.FetchRatesAsync(It.IsAny<string>(), It.IsAny<string>()), Times.Once);
        }
    }
}
