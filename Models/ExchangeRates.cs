using System.Text.Json.Serialization;

namespace CurrencyConverterApp.Models
{
    public class ExchangeRates
    {
        [JsonPropertyName("rates")]
        public Dictionary<string, decimal> Rates { get; set; }

        public ExchangeRates()
        {
            Rates = [];
        }
    }
}
