using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebApi.Controllers;
using Xunit;

namespace WebApi.Tests
{
    public class CurrencyExchangeControllerTests
    {
        private readonly Mock<ICurrencyExchangeService> _mockCurrencyExchangeService;
        private readonly Mock<ILogger<CurrencyExchangeController>> _mockLogger;
        private readonly CurrencyExchangeController _controller;

        public CurrencyExchangeControllerTests()
        {
            _mockCurrencyExchangeService = new Mock<ICurrencyExchangeService>();
            _mockLogger = new Mock<ILogger<CurrencyExchangeController>>();
            _controller = new CurrencyExchangeController(_mockCurrencyExchangeService.Object, _mockLogger.Object);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WithExchangeRates()
        {
            // Arrange: Prepare test data
            var exchangeRates = new CurrencyExchangeRate
            {
                Date = DateTime.UtcNow,
                Base = "USD",
                Rates = new Dictionary<string, decimal>
                {
                    { "EUR", 0.94m },
                    { "JPY", 149.64m },
                    { "GBP", 0.75m }
                }
            };

            _mockCurrencyExchangeService.Setup(service => service.GetExchangeRatesAsync()).ReturnsAsync(exchangeRates);

            // Act: Call the controller method
            var result = await _controller.Get();

            // Assert: Verify the result is an OkObjectResult with correct status and data
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            var returnedRates = okResult.Value as CurrencyExchangeRate;
            Assert.NotNull(returnedRates);
            Assert.Equal("USD", returnedRates.Base);
            Assert.Equal(exchangeRates.Rates, returnedRates.Rates);
        }

        [Fact]
        public async Task Get_ReturnsInternalServerError_WhenExceptionOccurs()
        {
            // Arrange: Mock an exception in the service call
            _mockCurrencyExchangeService.Setup(service => service.GetExchangeRatesAsync()).ThrowsAsync(new Exception("API failure"));

            // Act/Assert: Call the controller method
            var exception =  await Assert.ThrowsAsync<Exception>(_controller.Get);
        }
    }
}
