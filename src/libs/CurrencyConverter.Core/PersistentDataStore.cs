using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core
{
    public class PersistentDataStore : IPersistentDataStore
    {
        private readonly IDataProvider _dataProvider;
        private ExchangeDataSet DataSet { get; set; }

        public PersistentDataStore(IDataProvider dataProvider) {
            _dataProvider = dataProvider;
            Task.WaitAll(Task.Run(() => RefreshData()));
        }

        public decimal GetExchangeRatio(string currencyCode)
        {
            if (currencyCode.Length != 3) throw new ArgumentException($"Invalid currency code: '{currencyCode}'");

            var currency = DataSet.Currencies.FirstOrDefault(c => 
                c.Code.Equals(currencyCode, StringComparison.InvariantCultureIgnoreCase))
                    ?? throw new KeyNotFoundException($"Couldn't find currency with code '{currencyCode}'"); 

            return currency.ExchangeRatio;
        }

        public async Task RefreshData()
        {
            var exchangeData = await _dataProvider.LoadExchangeData();
            if (DataSet is null || DataSet.ActualAt < exchangeData.ActualAt) {
                DataSet = exchangeData;
            }
        }
    }
}