using MediatR;
using Microsoft.AspNetCore.Mvc;
using SortApp.Application.Mediator.Command;
using SortApp.Application.Mediator.Query;
using SortApp.Application.Model;

namespace SortApp.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SortingController : ControllerBase
    {
        private readonly ILogger<SortingController> _logger;
        private readonly IMediator _mediator;

        public SortingController(ILogger<SortingController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> SortArray(SortArrayCommand sortCommand)
        {
            try
            {
                await _mediator.Send(sortCommand);

                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during processing {nameof(SortArray)}", ex);
                return BadRequest();
            }
        }


        [HttpGet("lastSorting",Name = "LastSort")]
        public async Task<ActionResult<SortingResult>> GetLastSortingResult()
        {
            try
            {
                return await _mediator.Send(new GetLastSortingResultQuery());
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error during processing {nameof(GetLastSortingResult)}", ex);
                return BadRequest();
            }
        }
    }
}