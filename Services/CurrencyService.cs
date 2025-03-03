using CurrencyConverterApp.Models;
using CurrencyConverterApp.Services.Interface;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace CurrencyConverterApp.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly string _apiUrl;

        public CurrencyService(IConfiguration configuration)
        {
            _apiUrl = configuration["ApiSettings:ExchangeRateApiUrl"] ?? string.Empty;
        }

        public async Task<decimal> GetExchangeRateAsync(string moedaOrigem, string moedaDestino)
        {
            try
            {
                using HttpClient client = new();
                var response = await client.GetStringAsync(_apiUrl + moedaOrigem);
                var dados = JsonSerializer.Deserialize<ExchangeRates>(response);

                if (dados != null && dados.Rates.TryGetValue(moedaDestino, out decimal value))
                {
                    return value;
                }

                return 0;
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}

