using HotDesk.Api.Persistence.HotDesk.Entities;
using HotDesk.Api.Persistence.HotDesk.Entities.Enums;
using HotDesk.Api.UseCases.Locations.Commands.RemoveLocation;

namespace HotDesk.Tests.UseCases.Locations
{
    public class RemoveLocationTest : BaseTest
    {
        [Fact]
        public async Task When_LocationNotFound_ThrowArgumentNullException()
        {
            // Arrange
            var command = new RemoveLocationUseCase.Command(1);
            // Act
            // Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => mediator.Send(command));
        }
        [Fact]
        public async Task When_LocationHasDesksAssigned_ThrowException()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                Name = "Test",
                AddDate = DateTime.UtcNow.AddHours(-1),
                RemoveDate = null,
                Desks = new List<Desk>
                {
                    new Desk
                    {
                        Id = 1,
                        Status = DeskStatusEnum.Available,
                        AddDate = DateTime.UtcNow
                    }
                }
            };
            repository.Locations.Add(location);
            await repository.SaveChangesAsync();

            var command = new RemoveLocationUseCase.Command(1);
            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() => mediator.Send(command));
        }
        [Fact]
        public async Task When_LocationAlreadyRemoved_ThrowException()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                Name = "Test",
                AddDate = DateTime.UtcNow.AddHours(-1),
                RemoveDate = DateTime.UtcNow
            };
            repository.Locations.Add(location);
            await repository.SaveChangesAsync();

            var command = new RemoveLocationUseCase.Command(1);
            // Act
            // Assert
            await Assert.ThrowsAsync<Exception>(() => mediator.Send(command));
        }
        [Fact]
        public async Task Should_RemoveLocation()
        {
            // Arrange
            var location = new Location
            {
                Id = 1,
                Name = "Test",
                AddDate = DateTime.UtcNow,
                RemoveDate = null
            };
            repository.Locations.Add(location);
            await repository.SaveChangesAsync();

            var command = new RemoveLocationUseCase.Command(1);
            // Act
            await mediator.Send(command);
            // Assert
            var dbLocation = Assert.Single(repository.Locations);
            Assert.NotNull(dbLocation.RemoveDate);
        }
    }
}
