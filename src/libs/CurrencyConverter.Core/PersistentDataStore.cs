using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyConverter.Core.Models;

namespace CurrencyConverter.Core
{
    public class PersistentDataStore : IPersistentDataStore
    {
        private readonly IDataProvider _dataProvider;

        private ExchangeDataSet DataSet { get; set; }

        public PersistentDataStore(ExchangeDataSet dataSet) {
            DataSet = dataSet; 
        } 

        public PersistentDataStore(IDataProvider dataProvider) {
            DataSet = new ExchangeDataSet {
                Currencies = new List<Currency>(),
                ActualAt = default
            };
            
            _dataProvider = dataProvider;
            RefreshData();
        }

        public decimal GetExchangeRatio(string currencyCode)
        {
            if (currencyCode.Length != 3) throw new ArgumentException($"Invalid currency code: '{currencyCode}'");

            var currency = DataSet.Currencies.FirstOrDefault(c => c.Code.Equals(currencyCode, StringComparison.InvariantCultureIgnoreCase))
                ?? throw new KeyNotFoundException($"Couldn't find currency with code '{currencyCode}'"); 

            return currency.ExchangeRatio;
        }

        public void RefreshData()
        {
            var exchangeData = _dataProvider.LoadExchangeData();
            if (exchangeData != null && DataSet.ActualAt < exchangeData.ActualAt) {
                DataSet = exchangeData;
            }
        }
    }
}