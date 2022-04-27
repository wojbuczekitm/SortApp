using SortApp.Application.Model;

namespace SortApp.Application.Logic.SortingAlgorithm
{
    public class InsertSortAlgorithm : ISortingAlgorithm
    {
        public SortingAlgorithmEnum AlgorithmEnumType => SortingAlgorithmEnum.InsertSort;

        public SortingResult Sort(IEnumerable<long> numbers)
        {
            var array = numbers.ToArray();
            InsertSort(array);
            return new SortingResult
            {
                SortedData = array
            };
        }

        private void InsertSort(long[] array)
        {
            for (var i = 1; i < array.Length; i++)
            {
                var v = array[i];
                var j = i - 1;
                while (j >= 0 && array[j] > v)
                {
                    array[j + 1] = array[j];
                    j--;
                }
                array[j + 1] = v;
            }
        }
    }
}
