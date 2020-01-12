using CurrencyConverter.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCurrencyConverter(this IServiceCollection services, ExchangeDataSet dataSet) {
            services.AddSingleton<ICurrencyConverter, CurrencyConverter>(); 
            services.AddSingleton<IPersistentDataStore>(new PersistentDataStore(dataSet));
            return services;
        }
    }
}