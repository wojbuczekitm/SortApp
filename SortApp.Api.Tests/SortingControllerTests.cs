using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Newtonsoft.Json;
using SortApp.Application.Mediator.Command;
using SortApp.Application.Model;
using SortApp.Application.Service;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SortApp.Api.Tests
{
    public class SortingControllerTests
    {
        private WebApplicationFactory<Program> _factory;

        public SortingControllerTests()
        {
            _factory = new WebApplicationFactory<Program>();
        }


        [Theory]
        [InlineData(new long[] { 3, 21, 2, 1, 5, 6 }, SortingAlgorithmEnum.InsertSort)]
        [InlineData(new long[] { 3, 21, 2, 1, 5, 6 }, SortingAlgorithmEnum.MergeSort)]
        [InlineData(new long[] { 3, 21, 2, 1, 5, 6 }, SortingAlgorithmEnum.MergeSortInPlace)]
        public async Task SortArray_PassedCorrectRequest_ReturnsOk(IEnumerable<long> testData, SortingAlgorithmEnum algorithmEnum)
        {
            // arrange
            var client = _factory.CreateClient();

            var request = new SortArrayCommand
            {
                Numbers = testData,
                SortAlgorithm = algorithmEnum
            };

            var requestStr = JsonConvert.SerializeObject(request);
            var requestContent = new StringContent(requestStr, Encoding.Unicode, "application/json");

            // act
            var response = await client.PostAsync("/Sorting/SortArray", requestContent);

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData(new long[] { 3, 21, 2, 1, 5, 6 })]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6 })]
        public async Task GetLastSortingResult_PassedExampleDataToFileService_ReturnsThatsArray(IEnumerable<long> testData)
        {
            // arrange
            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(p => p.GetLastSavedFileContent())
                .Returns(testData);

            var client = _factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var fileService = services.SingleOrDefault(p => p.ServiceType == typeof(IFileService));

                    services.Remove(fileService);
                    services.AddScoped<IFileService>(p => fileServiceMock.Object);

                });
            })
            .CreateClient();

            // act
            var response = await client.GetAsync("/Sorting/lastSorting");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var contentStr = await response.Content.ReadAsStringAsync();
            var sortingResult = JsonConvert.DeserializeObject<SortingResult>(contentStr);

            sortingResult.SortedData.Should().BeEquivalentTo(testData).And.ContainInOrder(testData);
        }
    }
}