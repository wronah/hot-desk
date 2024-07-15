using HotDesk.Api.Common.Interfaces;
using HotDesk.Api.Persistence.HotDesk.Entities.Enums;
using MediatR;

namespace HotDesk.Api.UseCases.Desks.Commands.MakeDeskUnavailable
{
    public static class MakeDeskUnavailableUseCase
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
                var desk = repository.Desks
                    .FirstOrDefault(x => x.Id == request.Id) ?? throw new ArgumentNullException($"Did not find desk id: {request.Id}");
                if(desk.Status == DeskStatusEnum.Unavailable)
                {
                    throw new Exception("The desk is already unavailable");
                }
                desk.Status = DeskStatusEnum.Unavailable;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
