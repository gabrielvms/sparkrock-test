using Application.Services;
using Core.Entities;
using Core.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Application.Tests
{
    public class CurrencyExchangeServiceTests
    {
        private readonly Mock<ICacheService> _mockCacheService;
        private readonly Mock<ILogger<CurrencyExchangeService>> _mockLogger;
        private readonly CurrencyExchangeService _currencyExchangeService;

        public CurrencyExchangeServiceTests()
        {
            _mockCacheService = new Mock<ICacheService>();
            _mockLogger = new Mock<ILogger<CurrencyExchangeService>>();
            _currencyExchangeService = new CurrencyExchangeService(_mockCacheService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task GetExchangeRatesAsync_ReturnsExchangeRates()
        {
            // Arrange
            var expectedRates = new CurrencyExchangeRate
            {
                Date = DateTime.UtcNow,
                Base = "USD",
                Rates = new Dictionary<string, decimal>
            {
                { "EUR", 0.85M },
                { "JPY", 110.25M }
            }
            };

            _mockCacheService
                .Setup(service => service.GetOrSetCurrencyExchangeRate())
                .ReturnsAsync(expectedRates);

            // Act
            var result = await _currencyExchangeService.GetExchangeRatesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedRates.Date, result.Date);
            Assert.Equal(expectedRates.Base, result.Base);
            Assert.Equal(expectedRates.Rates, result.Rates);

            _mockCacheService.Verify(service => service.GetOrSetCurrencyExchangeRate(), Times.Once);
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Get data from ExchangeService")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task GetExchangeRatesAsync_ThrowsException_WhenCacheServiceFails()
        {
            // Arrange
            _mockCacheService
                .Setup(service => service.GetOrSetCurrencyExchangeRate())
                .ThrowsAsync(new InvalidOperationException("Cache service error"));

            // Act & Assert
            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _currencyExchangeService.GetExchangeRatesAsync());

            Assert.Equal("Cache service error", exception.Message);

            _mockCacheService.Verify(service => service.GetOrSetCurrencyExchangeRate(), Times.Once);
            _mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Get data from ExchangeService")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }

}
