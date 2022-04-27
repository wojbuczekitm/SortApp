using SortApp.Application.Model;

namespace SortApp.Application.Logic.SortingAlgorithm
{
    public class MergeSortInPlaceAlgorithm : ISortingAlgorithm
    {
        public SortingAlgorithmEnum AlgorithmEnumType => SortingAlgorithmEnum.MergeSortInPlace;

        public SortingResult Sort(IEnumerable<long> numbers)
        {
            var array = numbers.ToArray();
            var tmpArray = new long[array.Length];
            MergeSort(array, tmpArray, 0, numbers.Count() - 1);
            return new SortingResult
            {
                SortedData = array
            };
        }

        private void MergeSort(long[] array, long[] tmpArray, int idxA, int idxB)
        {
            if (idxA < idxB)
            {
                int idxQ = (idxA + idxB) / 2;
                MergeSort(array, tmpArray, idxA, idxQ);
                MergeSort(array, tmpArray, idxQ + 1, idxB);
                Merge(array, tmpArray, idxA, idxQ, idxB);
            }
        }

        private void Merge(long[] array, long[] tmpArray, int idxA, int idxQ, int idxB)
        {
            int i, j, k;

            var leftPartLength = idxQ - idxA + 1;
            var rightPartLength = idxB - idxQ;
            for (i = idxA; i < idxA + leftPartLength; i++)
            {
                tmpArray[i] = array[i];
            }
            for (i = idxQ + 1; i < idxQ + 1 + rightPartLength; i++)
            {
                tmpArray[i] = array[i];
            }

            i = j = 0;
            k = idxA;
            while (i < leftPartLength && j < rightPartLength)
            {
                if (tmpArray[idxA + i] <= tmpArray[idxQ + 1 + j])
                {
                    array[k] = tmpArray[idxA + i];
                    i++;
                }
                else
                {
                    array[k] = tmpArray[idxQ + 1 + j];
                    j++;
                }

                k++;
            }
            for (; i < leftPartLength; i++, k++)
            {
                array[k] = tmpArray[idxA + i];
            }
            for (; j < rightPartLength; j++, k++)
            {
                tmpArray[k] = tmpArray[idxQ + 1 + j];
            }
        }
    }
}
