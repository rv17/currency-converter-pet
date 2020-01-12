using System;
using System.Threading.Tasks;
using CurrencyConverter.Core.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace CurrencyConverter.Core
{
    public class FileSystemDataProvider : IDataProvider
    {
        private readonly IFileService _fileService;
        private readonly string _datasetFilePath;

        public FileSystemDataProvider(IFileService fileService, IConfiguration config) {
            if (config is null) throw new ArgumentNullException(nameof(config));
            _fileService = fileService;
            _datasetFilePath = config.GetSection("datasetFilePath")?.Value
                ?? throw new ArgumentException("Couldn't find dataset file path in configuration");
        }

        public async Task<ExchangeDataSet> LoadExchangeData()
        {
            var datasetFileContent = await _fileService.ReadToStringAsync(_datasetFilePath);
            var dataset = JsonConvert.DeserializeObject<ExchangeDataSet>(datasetFileContent);
            return dataset;
        }
    }
}