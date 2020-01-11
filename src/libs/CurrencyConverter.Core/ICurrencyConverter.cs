namespace CurrencyConverter.Core
{
    public interface ICurrencyConverter
    {
        decimal Convert(decimal value, decimal sourceRatio, decimal targetRatio);
    }
}