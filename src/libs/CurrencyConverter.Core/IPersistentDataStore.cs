using System.Threading.Tasks;

namespace CurrencyConverter.Core
{
    public interface IPersistentDataStore
    {
        decimal GetExchangeRatio(string currencyCode);

        Task RefreshData();
    }
}