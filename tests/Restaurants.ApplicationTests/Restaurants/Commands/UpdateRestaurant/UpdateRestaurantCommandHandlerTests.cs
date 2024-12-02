using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repository;
using Xunit;

namespace Restaurants.Application.Restaurants.Commands.UpdateRestaurant.Tests;

public class UpdateRestaurantCommandHandlerTests
{
    private readonly Mock<ILogger<UpdateRestaurantCommandHandler>> _loggerMock;
    private readonly Mock<IRestaurantsRepository> _restaurantsRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;

    private readonly UpdateRestaurantCommandHandler _handler;
    public UpdateRestaurantCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateRestaurantCommandHandler>>();
        _restaurantsRepositoryMock = new Mock<IRestaurantsRepository>();
        _mapperMock = new Mock<IMapper>();

        _handler = new UpdateRestaurantCommandHandler(
            _loggerMock.Object,
            _mapperMock.Object,
            _restaurantsRepositoryMock.Object);
    }

    [Fact()]
    public async Task Handle_ForValidCommands_ReturnsCreatedRestaurant()
    {
        // Arrange
        var restaurantId = 1;
        var command = new UpdateRestaurantCommand()
        {
            Id = restaurantId,
            Name = "New Test",
            Description = "New Description",
            HasDelivery = true,
        };
        var restaurant = new Restaurant()
        {
            Id = restaurantId,
            Name = "Test",
            Description = "Test",
        };
        _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
            .ReturnsAsync(restaurant);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _restaurantsRepositoryMock.Verify(r => r.Update(), Times.Once);
        _mapperMock.Verify(m => m.Map(command, restaurant), Times.Once);
    }

    [Fact]
    public async Task Handle_WithNonExistingRestaurant_ShouldThrowNotFoundException()
    {
        // Arrange
        var restaurantId = 2;
        var request = new UpdateRestaurantCommand
        {
            Id = restaurantId
        };

        _restaurantsRepositoryMock.Setup(r => r.GetByIdAsync(restaurantId))
                .ReturnsAsync((Restaurant?)null);
        // act
        Func<Task> act = async () => await _handler.Handle(request, CancellationToken.None);
        // assert
        await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Restaurant with id: {restaurantId} does not exist");
    }
}