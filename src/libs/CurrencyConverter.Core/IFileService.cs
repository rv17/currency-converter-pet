using System.Threading.Tasks;

namespace CurrencyConverter.Core
{
    public interface IFileService
    {
        Task<string> ReadToStringAsync(string path);
    }
}