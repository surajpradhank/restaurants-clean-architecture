using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repository;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.CreateRestaurant.Tests;

public class CreateRestaurantCommandHandlerTests
{
    [Fact()]
    public async Task Handle_ForValidCommands_ReturnsCreatedRestaurant()
    {
        //Arrange`
        var loggerMock = new Mock<ILogger<CreateRestaurantCommandHandler>>();

        var command = new CreateRestaurantCommand();
        var restaurant = new Restaurant();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Restaurant>(command)).Returns(restaurant);

        var restaurantRepositoryMock = new Mock<IRestaurantsRepository>();
        restaurantRepositoryMock
            .Setup(repo => repo.Create(It.IsAny<Restaurant>()))
            .ReturnsAsync(1);

        var commandHandler = new CreateRestaurantCommandHandler(loggerMock.Object,
                                                                mapperMock.Object,
                                                                restaurantRepositoryMock.Object);

        // Act
        var result = await commandHandler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().Be(1);
        restaurantRepositoryMock.Verify(r => r.Create(restaurant), Times.Once);
    }
}