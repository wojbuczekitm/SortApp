using MediatR;
using SortApp.Application.Logic.SortingAlgorithm;
using SortApp.Application.Model;
using SortApp.Application.Service;
using System.ComponentModel.DataAnnotations;

namespace SortApp.Application.Mediator.Command
{
    public class SortArrayCommand : IRequest
    {
        public IEnumerable<long> Numbers { get; set; }
        public SortingAlgorithmEnum SortAlgorithm { get; set; }
    }

    public class SortArrayCommandHandler : IRequestHandler<SortArrayCommand>
    {
        private readonly ISortingAlgorithmFactory _sortingAlgorithmFactory;
        private readonly IFileService _fileService;

        public SortArrayCommandHandler(
            ISortingAlgorithmFactory sortingAlgorithmFactory,
            IFileService fileService)
        {
            _sortingAlgorithmFactory = sortingAlgorithmFactory;
            _fileService = fileService;
        }

        public Task<Unit> Handle(SortArrayCommand request, CancellationToken cancellationToken)
        {
            var algorithm = _sortingAlgorithmFactory.GetSortingAlgorithm(request.SortAlgorithm);

            var sorted = algorithm.Sort(request.Numbers);

            _fileService.SaveNumberArrayToFile(sorted.SortedData);

            return Unit.Task;
        }
    }
}
