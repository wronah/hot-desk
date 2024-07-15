using HotDesk.Api.UseCases.GenerateJwtToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HotDesk.Api.Controllers
{
    [Route("api/[controller]")]
    public class JwtController : ControllerBase
    {
        private readonly IMediator mediator;

        public JwtController(IMediator mediator) 
        {
            this.mediator = mediator;
        }

        [HttpGet("by-user-id/{userId}")]
        public async Task<ActionResult> GenerateJwtToken(int userId, CancellationToken cancellationToken)
        {
            var command = new GenerateJwtTokenUseCase.Command(userId);
            var jwt = await mediator.Send(command, cancellationToken);
            return Ok(jwt);
        }
    }
}
