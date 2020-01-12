using System;
using System.Collections.Generic;

namespace CurrencyConverter.Core.Models
{
    public class ExchangeDataSet
    {
        public List<Currency> Currencies { get; set; } 
        public DateTime ActualAt { get; set; }
    }
}