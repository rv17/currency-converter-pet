using CurrencyConverter.Core;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CurrencyConverter.ConsoleApp
{
    public class Converter : IHostedService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly ICurrencyConverter _converter;
        private readonly IPersistentDataStore _dataStore;
        
        private bool _stop = false;

        public Converter(
            IHostApplicationLifetime appLifetime, 
            ICurrencyConverter converter, 
            IPersistentDataStore dataStore) 
        {
            _appLifetime = appLifetime;
            _converter = converter;
            _dataStore = dataStore;
        }

        public Task StartAsync(CancellationToken ct) {
            _appLifetime.ApplicationStarted.Register(OnStart);
            _appLifetime.ApplicationStopping.Register(OnStopping);
            _appLifetime.ApplicationStopped.Register(OnStopped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken ct) {
            return Task.CompletedTask;
        }

        private void OnStart() {
            Console.WriteLine("Currency converter");
            while (!_stop) Loop();
        }

        private void OnStopping() {
            Console.WriteLine("Stopping...");
        }

        private void OnStopped() {
            Console.WriteLine("Stopped.");
        }

        private void Loop() {
            Console.WriteLine("What do you want to convert? (/h - help, /q - quit, /l - see list of currencies)");
            
            string[] words = Console.ReadLine().Split(' ');

            if (words[0] == "/h") {
                Help();
            }
            else if (words[0] == "/q") {
                _stop = true;
                _appLifetime.StopApplication();
            }
            else if (words[0] == "/l") {
                Console.WriteLine("This feature is not implemented yet. Sorry for that");
            }
            else if (decimal.TryParse(words[0], out var value) && words.Length > 2) {
                var sourceCurrencyCode = words[1];
                var targetCurrencyCode = words[2];

                try { 
                    var sourceExchangeRatio = _dataStore.GetExchangeRatio(sourceCurrencyCode);
                    var targetExchangeRatio = _dataStore.GetExchangeRatio(targetCurrencyCode);

                    var convertedValue = _converter.Convert(value, sourceExchangeRatio, targetExchangeRatio);
                    Console.WriteLine($"{value} {sourceCurrencyCode} is {convertedValue} {targetCurrencyCode}");
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (KeyNotFoundException ex)
                {
                    Console.WriteLine($"{ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Something went wrong.\n{ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("I did not understand you.");
            }
        }
        
        private void Help() {
            var help = new StringBuilder()
                .AppendLine("Enter value and currency code to convert from and currency code to convert into.")
                .AppendLine("For example, if you want to convert 100 US Dollars into Euro, type the following:")
                .AppendLine("    100 usd eur    ")
                .AppendLine("Decimal point symbol is the period.")
                .AppendLine("    10.85 eur usd    ")
                .ToString();

            Console.WriteLine(help);
        }
    }
}