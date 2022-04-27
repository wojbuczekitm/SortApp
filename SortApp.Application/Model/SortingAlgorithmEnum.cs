using System.ComponentModel;

namespace SortApp.Application.Model
{
    public enum SortingAlgorithmEnum
    {
        [Description("Merge Sort")]
        MergeSort = 1,
        [Description("Insert Sort")]
        InsertSort = 3,
        [Description("Merge Sort In Place")]
        MergeSortInPlace = 4
    }
}
