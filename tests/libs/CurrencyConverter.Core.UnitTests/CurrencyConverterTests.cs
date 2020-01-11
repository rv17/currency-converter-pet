using NUnit.Framework;
using System;

namespace CurrencyConverter.Core.UnitTests
{
    public class CurrencyConverterTests
    {
        #region privates, setup & teardown
        private ICurrencyConverter _converter;

        [SetUp]
        public void BeforeEach() {
            _converter = new CurrencyConverter();
        }

        [TearDown]
        public void AfterEach() {
            _converter = null;
        }
        #endregion

        /* Exchange formula:
         * destValue = sourceValue * sourceRatio / destRatio
         */
        
        [Test]
        public void ConvertTest_ReceivesCorrectData_ReturnsConvertedValue() {
            decimal sourceValue = 1m;
            decimal sourceRatio = 5m;
            decimal destRatio = 2m;

            decimal expectedValue = 2.5m;  

            decimal actualValue = _converter.Convert(sourceValue, sourceRatio, destRatio);

            Assert.That(actualValue, Is.EqualTo(expectedValue));
        }

        [Test]
        public void ConvertTest_ReceivesNegativeSourceRatio_ThrowsArgumentOutOfRangeException() =>
            NonPositiveRatiosTestHelper(-1m, 5m);

        [Test]
        public void ConvertTest_ReceivesNegativeDestRatio_ThrowsArgumentOutOfRangeException() =>
            NonPositiveRatiosTestHelper(5m, -1m);

        [Test]
        public void ConvertTest_ReceivesZeroSourceRatio_ThrowsArgumentOutOfRangeException() =>
            NonPositiveRatiosTestHelper(0, 5m);

        [Test]
        public void ConvertTest_ReceivesZeroDestRatio_ThrowsArgumentOutOfRangeException() =>
            NonPositiveRatiosTestHelper(5m, 0);

        private void NonPositiveRatiosTestHelper(decimal sourceRatio, decimal destRatio) {
            decimal sourceValue = 1m;

            Assert.Throws<ArgumentOutOfRangeException>(
                () => _converter.Convert(sourceValue, sourceRatio, destRatio)
            );
        }
    }
}