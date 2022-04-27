using System.Collections.Generic;
using Xunit;
using SortApp.Application.Logic.SortingAlgorithm;
using FluentAssertions;

namespace SortApp.Application.Tests.SortingAlgorithms
{
    public class MergeSortAlgorithmTests
    {
        [Theory]
        [InlineData(new long[] { 2, 4, 1, 3 })]
        [InlineData(new long[] { 10, 5, 8, 1, 3, 2, 7, 4 })]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7 })]
        [InlineData(new long[] { 1, 2, 3, 54, 4, 5, 6, 7 })]
        public void Sort_PassedDisorderedArray_ResultIsSortedArray(IEnumerable<long> array)
        {
            // arange
            var algorithm = new MergeSortAlgorithm();

            //act
            var result = algorithm.Sort(array);

            //assert
            result.Should().NotBeNull();
            result.SortedData.Should().BeEquivalentTo(array).And.BeInAscendingOrder();
        }
    }
}