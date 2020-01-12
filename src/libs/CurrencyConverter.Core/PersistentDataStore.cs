using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core
{
    public class PersistentDataStore : IPersistentDataStore
    {
        private ExchangeDataSet DataSet { get; set; }

        public PersistentDataStore(ExchangeDataSet dataSet) {
            DataSet = dataSet; 
        } 

        public decimal GetExchangeRatio(string currencyCode)
        {
            if (currencyCode.Length != 3) throw new ArgumentException($"Invalid currency code: '{currencyCode}'");

            var currency = DataSet.Currencies.FirstOrDefault(c => c.Code.Equals(currencyCode, StringComparison.InvariantCultureIgnoreCase))
                ?? throw new KeyNotFoundException($"Couldn't find currency with code '{currencyCode}'"); 

            return currency.ExchangeRatio;
        }
    }
}