using HotDesk.Api.Persistence.HotDesk.Entities.Enums;
using HotDesk.Api.Persistence.HotDesk.Entities;
using HotDesk.Api.UseCases.Desks.Commands.AddAndAssignDesk;

namespace HotDesk.Tests.UseCases.Desks
{
    public class AddAndAssignDeskTest : BaseTest
    {
        [Fact]
        public async Task Should_AddAndAssignNewDesk()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                Name = "Test",
                AddDate = DateTime.UtcNow.AddHours(-1),
                RemoveDate = null,
            };
            repository.Locations.Add(location);
            await repository.SaveChangesAsync();

            var command = new AddAndAssignDeskUseCase.Command(1);
            // Act
            await mediator.Send(command);
            // Assert
            var addedDesk = Assert.Single(repository.Desks);
            Assert.Single(location.Desks!);
            Assert.Equal(1, addedDesk.Id);
            Assert.Equal(1, addedDesk.LocationId);
            Assert.Equal(DeskStatusEnum.Available, addedDesk.Status);
        }
    }
}
