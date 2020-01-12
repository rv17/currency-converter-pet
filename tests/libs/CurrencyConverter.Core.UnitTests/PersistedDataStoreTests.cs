using System;
using System.Collections.Generic;
using System.Linq;
using CurrencyConverter.Core.Models;
using NUnit.Framework;

namespace CurrencyConverter.Core.UnitTests
{
    public class PersistedDataStoreTests
    {
        #region privates, setup & teardown
        private IPersistentDataStore _store;

        private ExchangeDataSet TestDataSet => new ExchangeDataSet {
            Currencies = new List<Currency> {
                new Currency { Code = "usd", ExchangeRatio = 1m },
                new Currency { Code = "eur", ExchangeRatio = 1.15m },
                new Currency { Code = "rub", ExchangeRatio = 0.016m },
            },
            ActualAt = DateTime.Parse("2020-01-10")
        };

        [SetUp]
        public void BeforeEach() {
            _store = new PersistentDataStore(TestDataSet);
        }

        [TearDown]
        public void AfterEach() {
            _store = null;
        }
        #endregion

        [TestCase("usd")]
        [TestCase("EUR")]
        [TestCase("RuB")]
        public void GetExchangeRatioTest_ReceivesValidCurrencyCode_ReturnsExchangeRate(string code) {
            decimal expectedRatio = TestDataSet.Currencies
                .FirstOrDefault(c => c.Code == code.ToLowerInvariant())?.ExchangeRatio 
                    ?? throw new ArgumentException($"couldn't find currency '{code}' in test dataset", code);
            
            var actualRatio = _store.GetExchangeRatio(code);

            Assert.That(actualRatio, Is.EqualTo(expectedRatio));
        }

        [TestCase("nev")]
        [TestCase("NEV")]
        [TestCase("NeV")]
        public void GetExchangeRatioTest_ReceivesNonExistingCurrencyCode_ThrowsKeyNotFoundException(string code) {
            Assert.Throws<KeyNotFoundException>(() => _store.GetExchangeRatio(code));
        }

        [TestCase("invalid")]
        [TestCase("$")]
        [TestCase("")]
        public void GetExchangeRatioTest_ReceivesInvalidCurrencyCode_ThrowsArgumentException(string code) {
            Assert.Throws<ArgumentException>(() => _store.GetExchangeRatio(code));
        }
    }
}