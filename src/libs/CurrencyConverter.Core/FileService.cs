using System.IO;
using System.Threading.Tasks;

namespace CurrencyConverter.Core
{
    public class FileService : IFileService
    {
        public async Task<string> ReadToStringAsync(string path)
        {
            using var file = File.OpenRead(path);
            using var reader = new StreamReader(file);
            var content = await reader.ReadToEndAsync();
            return content;
        }
    }
}