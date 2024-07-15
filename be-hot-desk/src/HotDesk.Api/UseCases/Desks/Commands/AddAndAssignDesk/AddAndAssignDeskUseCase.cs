using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Persistence.HotDesk.Entities;
using HotDesk.Api.Persistence.HotDesk.Entities.Enums;
using MediatR;

namespace HotDesk.Api.UseCases.Desks.Commands.AddAndAssignDesk
{
    public static class AddAndAssignDeskUseCase
    {
        public record Command(int LocationId) : IRequest;
        internal class Handler : IRequestHandler<Command>
        {
            private readonly IRepository repository;

            public Handler(IRepository repository)
            {
                this.repository = repository;
            }
            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                var desk = new Desk
                {
                    Status = DeskStatusEnum.Available,
                    AddDate = DateTime.UtcNow,
                    LocationId = request.LocationId,
                };
                await repository.Desks.AddAsync(desk, cancellationToken);
                await repository.SaveChangesAsync(cancellationToken);    
            }
        }
    }
}
