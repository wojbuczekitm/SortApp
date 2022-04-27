using SortApp.Application.Model;

namespace SortApp.Application.Logic.SortingAlgorithm
{
    public class MergeSortAlgorithm : ISortingAlgorithm
    {
        public SortingAlgorithmEnum AlgorithmEnumType => SortingAlgorithmEnum.MergeSort;

        public SortingResult Sort(IEnumerable<long> numbers)
        {
            var array = numbers.ToArray();
            MergeSort(array, 0, numbers.Count() - 1);
            return new SortingResult
            {
                SortedData = array
            };
        }

        private void MergeSort(long[] array, int idxA, int idxB)
        {
            if(idxA < idxB)
            {
                int idxQ = (idxA + idxB) / 2;
                MergeSort(array, idxA, idxQ);
                MergeSort(array, idxQ+1, idxB);
                Merge(array, idxA, idxQ, idxB);
            }
        }

        private void Merge(long[] array, int idxA, int idxQ, int idxB)
        {
            int i, j, k;
            var leftPart = array.Skip(idxA).Take(idxQ - idxA + 1).ToArray();
            var rightPart = array.Skip(idxQ+1).Take(idxB - idxQ).ToArray();

            i = j = 0;
            k = idxA;
            while (i < leftPart.Length && j < rightPart.Length)
            {   
                if(leftPart[i] <= rightPart[j])
                {
                    array[k] = leftPart[i];
                    i++;
                }
                else
                {
                    array[k] = rightPart[j];
                    j++;
                }

                k++;
            }
            for (; i < leftPart.Length; i++, k++)
            {
                array[k] = leftPart[i];
            }
            for (; j < rightPart.Length; j++, k++)
            {
                array[k] = rightPart[j];
            }
        }
    }
}
