﻿using Application.Services;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CurrencyExchangeController : ControllerBase
    {
        private readonly CurrencyExchangeService _currencyExchangeService;
        private readonly ILogger<CurrencyExchangeController> _logger;
        public CurrencyExchangeController(CurrencyExchangeService currencyExchangeService, ILogger<CurrencyExchangeController> logger)
        {
            _currencyExchangeService = currencyExchangeService;
            _logger = logger;
        }
        // GET: api/<CurrencyExchangeController>
        [HttpGet]
        [ProducesResponseType(200, Type = typeof(CurrencyExchangeRate))]
        [ProducesResponseType(500)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Fetching exchange rates");

            var rates = await _currencyExchangeService.GetExchangeRatesAsync();

            _logger.LogInformation("Exchange rates fetched successfully.");
            return Ok(rates);
        }

    }
}
