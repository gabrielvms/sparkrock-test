using Core.Entities;

public interface ICurrencyExchangeService
{
    Task<CurrencyExchangeRate> GetExchangeRatesAsync();
}