using HotDesk.Api.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HotDesk.Api.UseCases.Locations.Commands.RemoveLocation
{
    public static class RemoveLocationUseCase
    {
        public record Command(int Id) : IRequest;
        internal class Handler : IRequestHandler<Command>
        {
            private readonly IRepository repository;

            public Handler(IRepository repository)
            {
                this.repository = repository;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var location = repository.Locations
                    .Include(x => x.Desks)
                    .FirstOrDefault(x => x.Id == request.Id) ?? throw new ArgumentNullException($"Did not find location id: {request.Id}");
                if(location.RemoveDate != null)
                {
                    throw new Exception("Location is already removed");
                }
                if(location.Desks!.Count > 0)
                {
                    throw new Exception("Cannot remove the location if it has desks assigned");
                }
                location.RemoveDate = DateTime.UtcNow;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
