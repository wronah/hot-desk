using HotDesk.Api.UseCases.Locations.Commands.AddLocation;

namespace HotDesk.Tests.UseCases.Locations
{
    public class AddLocationTest : BaseTest
    {
        [Fact]
        public async Task Should_AddNewLocation()
        {
            // Arrange
            var locationName = "Test";
            var command = new AddLocationUseCase.Command(locationName);
            // Act
            await mediator.Send(command);
            // Assert
            var addedLocation = Assert.Single(repository.Locations);
            Assert.Equal(1, addedLocation.Id);
            Assert.Equal(locationName, addedLocation.Name);
        }
    }
}
