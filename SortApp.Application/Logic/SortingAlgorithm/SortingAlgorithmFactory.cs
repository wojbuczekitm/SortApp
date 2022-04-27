using SortApp.Application.Model;

namespace SortApp.Application.Logic.SortingAlgorithm
{
    public class SortingAlgorithmFactory : ISortingAlgorithmFactory
    {
        private readonly IEnumerable<ISortingAlgorithm> _sortingAlgorithms;

        public SortingAlgorithmFactory(IEnumerable<ISortingAlgorithm> sortingAlgorithms)
        {
            _sortingAlgorithms = sortingAlgorithms;
        }

        public ISortingAlgorithm GetSortingAlgorithm(SortingAlgorithmEnum sortingAlgorithmEnum)
        {
            if (_sortingAlgorithms.Count(p => p.IsExpectedAlgorithm(sortingAlgorithmEnum)) > 1)
            {
                throw new Exception($"Found more than one algorithm with passsed {sortingAlgorithmEnum} type");
            }

            if (!_sortingAlgorithms.Any(p => p.IsExpectedAlgorithm(sortingAlgorithmEnum)))
            {
                throw new Exception($"Not found algorithm with passsed {sortingAlgorithmEnum} type");
            }

            return _sortingAlgorithms.First(p => p.IsExpectedAlgorithm(sortingAlgorithmEnum));
        }
    }
}
