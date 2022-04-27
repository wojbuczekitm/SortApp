using System.Collections.Generic;
using Xunit;
using SortApp.Application.Logic.SortingAlgorithm;
using FluentAssertions;
using System.Linq;
using System;

namespace SortApp.Application.Tests.SortingAlgorithms
{
    public class MergeSortInPlaceAlgorithmTests
    {
        [Theory]
        [InlineData(new long[] { 2, 4, 1, 3 })]
        [InlineData(new long[] { 10, 5, 8, 1, 3, 2, 7, 4 })]
        [InlineData(new long[] { 1, 2, 3, 4, 5, 6, 7 })]
        [InlineData(new long[] { 1, 2, 3, 54, 4, 5, 6, 7 })]
        public void Sort_PassedDisorderedArray_ResultIsSortedArray(IEnumerable<long> array)
        {
            // arrange
            var algorithm = new MergeSortInPlaceAlgorithm();

            // act
            var result = algorithm.Sort(array);

            //assert
            result.Should().NotBeNull();
            result.SortedData.Should().BeEquivalentTo(array).And.BeInAscendingOrder();
        }
    }
}