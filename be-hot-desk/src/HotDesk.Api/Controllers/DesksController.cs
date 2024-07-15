using HotDesk.Api.UseCases.Desks.Commands.AddAndAssignDesk;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotDesk.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class DesksController : ControllerBase
    {
        private readonly IMediator mediator;

        public DesksController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost("to-location/{locationId}")]
        public async Task<ActionResult> AddAndAssignDesk(int locationId, CancellationToken cancellationToken)
        {
            var command = new AddAndAssignDeskUseCase.Command(locationId);
            await mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
