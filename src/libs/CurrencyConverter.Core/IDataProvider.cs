using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core
{
    public interface IDataProvider
    {
        ExchangeDataSet LoadExchangeData();
    }
}