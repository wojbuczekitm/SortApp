namespace SortApp.Application.Logic.SortingAlgorithm
{
    using SortApp.Application.Model;

    public interface ISortingAlgorithm
    {
        SortingAlgorithmEnum AlgorithmEnumType { get; }
        
        bool IsExpectedAlgorithm(SortingAlgorithmEnum sortingAlgorithmEnum) 
            => AlgorithmEnumType == sortingAlgorithmEnum;

        SortingResult Sort(IEnumerable<long> numbers);
    }
}
