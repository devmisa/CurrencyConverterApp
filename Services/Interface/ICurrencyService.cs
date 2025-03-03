namespace CurrencyConverterApp.Services.Interface
{
    public interface ICurrencyService
    {
        Task<decimal> GetExchangeRateAsync(string moedaOrigem, string moedaDestino);
    }
}
