using System;

namespace CurrencyConverter.Core
{
    public class CurrencyConverter : ICurrencyConverter
    {
        public decimal Convert(decimal value, decimal sourceRatio, decimal targetRatio)
        {
            if (sourceRatio <= 0) throw new ArgumentOutOfRangeException(nameof(sourceRatio));
            if (targetRatio <= 0) throw new ArgumentOutOfRangeException(nameof(targetRatio));

            return value * sourceRatio / targetRatio;
        }
    }
}