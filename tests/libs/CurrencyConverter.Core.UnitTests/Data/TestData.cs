using CurrencyConverter.Core.Models;
using System;
using System.Collections.Generic;

namespace CurrencyConverter.Core.UnitTests.Data
{
    public static class TestData
    {
        public static ExchangeDataSet DefaultDataSet => new ExchangeDataSet {
            Currencies = new List<Currency> {
                new Currency { Code = "usd", ExchangeRatio = 1m },
                new Currency { Code = "eur", ExchangeRatio = 1.15m },
                new Currency { Code = "rub", ExchangeRatio = 0.016m },
            },
            ActualAt = DateTime.Parse("2020-01-10")
        };

        public static ExchangeDataSet UpdatedDataSet => new ExchangeDataSet {
            Currencies = new List<Currency> {
                new Currency { Code = "usd", ExchangeRatio = 1m },
                new Currency { Code = "eur", ExchangeRatio = 1.14m },
                new Currency { Code = "rub", ExchangeRatio = 0.0165m },
            },
            ActualAt = DateTime.Parse("2020-01-11")
        };

        public static string DefaultDataSetJson => 
            @"{
                ""currencies"": [
                    { ""code"": ""usd"", ""exchangeRatio"": 1 },
                    { ""code"": ""eur"", ""exchangeRatio"": 1.15 },
                    { ""code"": ""rub"", ""exchangeRatio"": 0.016 },
                ],
                ""actualAt"": ""2020-01-10T00:00:00""
            }";

        public static string UpdatedDataSetJson => 
            @"{
                ""currencies"": [
                    { ""code"": ""usd"", ""exchangeRatio"": 1 },
                    { ""code"": ""eur"", ""exchangeRatio"": 1.14 },
                    { ""code"": ""rub"", ""exchangeRatio"": 0.0165 },
                ],
                ""actualAt"": ""2020-01-11T00:00:00""
            }";
    }
}