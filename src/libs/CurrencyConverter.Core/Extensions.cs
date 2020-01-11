using Microsoft.Extensions.DependencyInjection;

namespace CurrencyConverter.Core
{
    public static class Extensions
    {
        public static IServiceCollection AddCurrencyConverter(this IServiceCollection services) =>
            services.AddSingleton<ICurrencyConverter, CurrencyConverter>(); 
    }
}