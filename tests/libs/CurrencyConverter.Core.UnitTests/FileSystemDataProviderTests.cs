using CurrencyConverter.Core.UnitTests.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CurrencyConverter.Core.UnitTests
{
    public class FileSystemDataProviderTests
    {
        #region privates, setup & teardown
        private IFileService _fileService;
        private IConfigurationSection _pathConfigSection;
        private IConfiguration _config;
        private IDataProvider _provider;

        [SetUp]
        public void BeforeEach() {
            _fileService = Substitute.For<IFileService>();
            _fileService.ReadToStringAsync(Arg.Any<string>())
                .ReturnsForAnyArgs(TestData.DefaultDataSetJson);

            _pathConfigSection = Substitute.For<IConfigurationSection>();
            _pathConfigSection.Value.Returns("dataset.json");
            _config = Substitute.For<IConfiguration>();
            _config.GetSection("datasetFilePath").Returns(_pathConfigSection);

            _provider = new FileSystemDataProvider(_fileService, _config);
        }

        [TearDown]
        public void AfterEach() {
            _provider = null;
            _fileService = null;
            _config = null;
            _pathConfigSection = null;
        }
        #endregion

        [Test]
        public async Task LoadExchangeDataTest_FoundDataFile_ReturnsDataSet() {
            var expected = JsonConvert.SerializeObject(TestData.DefaultDataSet);
            
            var dataset = await _provider.LoadExchangeData();

            var actual = JsonConvert.SerializeObject(dataset);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void LoadExchangeDataTest_NotFoundDataFile_ThrowsException() {
            _fileService.WhenForAnyArgs(async s => await s.ReadToStringAsync(Arg.Any<string>()))
                .Throw<FileNotFoundException>();

            Assert.ThrowsAsync<FileNotFoundException>(async () => await _provider.LoadExchangeData());
        }
    }
}