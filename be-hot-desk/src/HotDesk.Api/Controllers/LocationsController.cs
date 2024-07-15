﻿using HotDesk.Api.UseCases.Locations.Commands.AddLocation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotDesk.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class LocationsController : ControllerBase
    {
        private readonly IMediator mediator;

        public LocationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> AddLocation([FromBody] AddLocationUseCase.Command command, CancellationToken cancellationToken)
        {
            await mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}
