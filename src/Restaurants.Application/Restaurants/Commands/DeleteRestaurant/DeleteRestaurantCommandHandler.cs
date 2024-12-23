﻿using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant;

internal class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommand> logger,
                                              IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand>
{
    public async Task Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Deleting restaurant with id : {request.Id}");

        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id) ??
                    throw new NotFoundException(nameof(Restaurant), request.Id.ToString());

        await restaurantsRepository.Delete(restaurant);
    }
}
