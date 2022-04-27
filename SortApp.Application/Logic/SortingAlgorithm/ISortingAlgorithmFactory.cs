using SortApp.Application.Model;

namespace SortApp.Application.Logic.SortingAlgorithm
{
    public interface ISortingAlgorithmFactory
    {
        ISortingAlgorithm GetSortingAlgorithm(SortingAlgorithmEnum sortingAlgorithmEnum);
    }
}