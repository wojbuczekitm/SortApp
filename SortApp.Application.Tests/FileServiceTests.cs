using FluentAssertions;
using Newtonsoft.Json;
using SortApp.Application.Config;
using SortApp.Application.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SortApp.Application.Tests
{
    public class FileServiceTests
    {
        [Theory]
        [InlineData(new long[] { 3, 21, 2, 1, 5, 6 })]
        [InlineData(new long[] { 4, 2, 1, 3, 2 })]
        public void SaveNumberArrayToFile_PassedTestArray_SavedFile(IEnumerable<long> testData)
        {
            // arrange
            var config = new FileServiceConfig
            {
                DirPath = "",
                FileName = "TestFile"
            };

            var fileService = new FileService(config);
            var filePath = Path.Combine(config.DirPath, config.FileName);

            // act
            fileService.SaveNumberArrayToFile(testData);

            // assert
            var fileExists = File.Exists(filePath);

            fileExists.Should().BeTrue();
            var fileContentStr = File.ReadAllText(filePath);
            var arrayFromFile = fileContentStr.Split().Select(p=>long.Parse(p));

            arrayFromFile.Should().BeEquivalentTo(testData).And.ContainInOrder(testData);

            if (fileExists)
            {
                File.Delete(filePath);
            }
        }

        [Theory]
        [InlineData(new long[] { 3, 21, 2, 1, 5, 6 })]
        [InlineData(new long[] { 4, 2, 1, 3, 2 })]
        public void GetLastSavedFileContent_PassedTestArray_ReturnsTheSameArrayFromFile(IEnumerable<long> testData)
        {
            // arrange
            var config = new FileServiceConfig
            {
                DirPath = "",
                FileName = "TestFile"
            };

            var fileService = new FileService(config);
            var filePath = Path.Combine(config.DirPath, config.FileName);

            File.WriteAllText(filePath, string.Join(" ", testData));

            // act
            var result = fileService.GetLastSavedFileContent();

            // assert

            result.Should().NotBeNull();
            result.Should().HaveCount(testData.Count());
            result.Should().BeEquivalentTo(testData).And.ContainInOrder(testData);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
