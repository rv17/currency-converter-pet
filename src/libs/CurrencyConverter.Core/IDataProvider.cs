using System.Threading.Tasks;
using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core
{
    public interface IDataProvider
    {
        Task<ExchangeDataSet> LoadExchangeData();
    }
}