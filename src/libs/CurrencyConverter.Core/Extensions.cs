using CurrencyConverter.Core.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCurrencyConverter(this IServiceCollection services) {
            services.AddSingleton<ICurrencyConverter, CurrencyConverter>(); 
            services.AddSingleton<IPersistentDataStore, PersistentDataStore>();
            services.AddSingleton<IDataProvider, FileSystemDataProvider>();
            services.AddSingleton<IFileService, FileService>();
            return services;
        }
    }
}