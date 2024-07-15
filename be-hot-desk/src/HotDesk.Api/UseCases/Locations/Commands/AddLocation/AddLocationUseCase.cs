using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Persistence.HotDesk.Entities;
using MediatR;

namespace HotDesk.Api.UseCases.Locations.Commands.AddLocation
{
    public static class AddLocationUseCase
    {
        public record Command(string Name) : IRequest;
        internal class Handler : IRequestHandler<Command>
        {
            private readonly IRepository repository;

            public Handler(IRepository repository)
            {
                this.repository = repository;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var location = new Location
                {
                    Name = request.Name,
                    AddDate = DateTime.UtcNow,
                    RemoveDate = null,
                };
                await repository.Locations.AddAsync(location, cancellationToken);
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
