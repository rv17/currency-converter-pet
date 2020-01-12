using CurrencyConverter.Core.UnitTests.Data;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CurrencyConverter.Core.UnitTests
{
    public class PersistentDataStoreTests
    {
        #region privates, setup & teardown
        private IDataProvider _dataProvider;
        private IPersistentDataStore _store;

        [SetUp]
        public void BeforeEach() {
            _dataProvider = Substitute.For<IDataProvider>();
            _dataProvider.LoadExchangeData().Returns(Task.FromResult(TestData.DefaultDataSet));
            _store = new PersistentDataStore(_dataProvider);
        }

        [TearDown]
        public void AfterEach() {
            _dataProvider = null;
            _store = null;
        }
        #endregion

        [TestCase("usd")]
        [TestCase("EUR")]
        [TestCase("RuB")]
        public void GetExchangeRatioTest_ReceivesValidCurrencyCode_ReturnsExchangeRate(string code) {
            decimal expectedRatio = TestData.DefaultDataSet.Currencies
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

        [Test]
        public void RefreshDataTest_UpdatesDataSet() {            
            const string code = "usd";
            _dataProvider.LoadExchangeData().Returns(TestData.UpdatedDataSet);
            var expectedUsdRatio = TestData.UpdatedDataSet.Currencies.FirstOrDefault(c => c.Code == code)?.ExchangeRatio
                ?? throw new ArgumentException($"couldn't find currency '{code}' in updated dataset", code);
            
            _store.RefreshData();

            var actualUsdRatio = _store.GetExchangeRatio(code);
            Assert.That(actualUsdRatio, Is.EqualTo(expectedUsdRatio));
        }
    }
}