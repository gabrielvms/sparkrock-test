namespace Core.Entities
{
    public class CurrencyExchangeRate
    {
        public DateTime Date { get; set; }
        public string Base { get; set; } = null!;
        public IDictionary<string, decimal> Rates { get; set; } = null!;
    }
}
