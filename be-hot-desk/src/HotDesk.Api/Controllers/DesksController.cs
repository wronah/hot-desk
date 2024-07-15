using HotDesk.Api.UseCases.Desks.Commands.AddAndAssignDesk;
using HotDesk.Api.UseCases.Desks.Commands.MakeDeskUnavailable;
using HotDesk.Api.UseCases.Desks.Commands.RemoveDesk;
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

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoveDesk(int id, CancellationToken cancellationToken)
        {
            var command = new RemoveDeskUseCase.Command(id);
            await mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("make-unavailable/{id}")]
        public async Task<ActionResult> MakeDeskUnavailable(int id, CancellationToken cancellationToken)
        {
            var command = new MakeDeskUnavailableUseCase.Command(id);
            await mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
