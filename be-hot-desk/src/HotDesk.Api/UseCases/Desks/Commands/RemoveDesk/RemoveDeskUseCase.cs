using HotDesk.Api.Common.Interfaces;
using MediatR;

namespace HotDesk.Api.UseCases.Desks.Commands.RemoveDesk
{
    public static class RemoveDeskUseCase
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
                if (desk.RemoveDate != null)
                {
                    throw new Exception("Desk is already removed");
                }
                if (desk.StartReservationDate != null || desk.EndReservationDate != null)
                {
                    throw new Exception("Cannot remove the desk if it is reserved");
                }
                desk.RemoveDate = DateTime.UtcNow;
                await repository.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
