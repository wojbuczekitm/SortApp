using MediatR;
using SortApp.Application.Model;
using SortApp.Application.Service;

namespace SortApp.Application.Mediator.Query
{
    public class GetLastSortingResultQuery : IRequest<SortingResult>
    {
    }

    public class GetLastSortingResultQueryHandler : IRequestHandler<GetLastSortingResultQuery, SortingResult>
    {
        private readonly IFileService _fileService;

        public GetLastSortingResultQueryHandler(IFileService fileService)
        {
            _fileService = fileService;
        }

        public Task<SortingResult> Handle(
            GetLastSortingResultQuery request,
            CancellationToken cancellationToken)
        {
            return Task.FromResult(new SortingResult
            {
                SortedData = _fileService.GetLastSavedFileContent()
            });
        }
    }
}
