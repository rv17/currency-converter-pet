namespace CurrencyConverter.Core
{
    public interface IPersistentDataStore
    {
        decimal GetExchangeRatio(string currencyCode);
    }
}