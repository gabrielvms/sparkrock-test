using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly ICurrencyExchangeService _currencyExchangeService;
        private readonly ILogger<CurrencyExchangeController> _logger;
        public CurrencyExchangeController(ICurrencyExchangeService currencyExchangeService, ILogger<CurrencyExchangeController> logger)
        {
            _currencyExchangeService = currencyExchangeService;
            _logger = logger;
        }
        // GET: api/<CurrencyExchangeController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CurrencyExchangeRate))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get([FromQuery] string baseCurrency = "USD")
        {
            _logger.LogInformation("Get data for {type}", nameof(CurrencyExchangeRate));

            var rates = await _currencyExchangeService.GetExchangeRatesAsync(baseCurrency);

            _logger.LogInformation("Successfully got data for {type}", nameof(CurrencyExchangeRate));
            return Ok(rates);
        }

    }
}
