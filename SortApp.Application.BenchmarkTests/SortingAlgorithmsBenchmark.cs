using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using SortApp.Application.Logic.SortingAlgorithm;

namespace SortApp.Application.BenchmarkTests
{
    [MemoryDiagnoser]
    [Orderer(SummaryOrderPolicy.FastestToSlowest)]
    [RankColumn]
    public class SortingAlgorithmsBenchmark
    {
        [Params(100, 10000, 100000)]
        public int TestArraySize;

        private long[] _testArray;

        [GlobalSetup]
        public void GlobalSetup()
        {
            _testArray = Enumerable.Range(0, TestArraySize)
                .Select(r => (long)r)
                .Reverse()
                .ToArray();
            
            Replace(_testArray, TestArraySize / 4, TestArraySize / 3);
            Replace(_testArray, TestArraySize / 2, TestArraySize - 1);
        }

        private void Replace(long[] arr, int idx1, int idx2)
        {
            var x = arr[idx1];
            arr[idx1] = arr[idx2];
            arr[idx2] = x;
        }

        [Benchmark]
        public void DotNetSort()
        {
            var sorted = _testArray.OrderBy(p => p).ToList();
        }

        [Benchmark]
        public void InsertSort()
        {
            var algorithm = new InsertSortAlgorithm();
            algorithm.Sort(_testArray);
        }

        [Benchmark]
        public void MergeSort()
        {
            var algorithm = new MergeSortAlgorithm();
            algorithm.Sort(_testArray);
        }

        [Benchmark]
        public void MergeSortInPlace()
        {
            var algorithm = new MergeSortInPlaceAlgorithm();
            algorithm.Sort(_testArray);
        }
    }
}
