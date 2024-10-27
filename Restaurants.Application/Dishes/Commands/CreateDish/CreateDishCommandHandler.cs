using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Exceptions;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Dishes.Commands.CreateDish;

internal class CreateDishCommandHandler(ILogger<CreateDishCommandHandler> logger,
                                        IRestaurantsRepository restaurantsRepository,
                                        IDishesRepository dishesRepository,
                                        IMapper mapper) : IRequestHandler<CreateDishCommand, int>
{
    public async Task<int> Handle(CreateDishCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Creatin new dish:{@DishRequest}", request);

        var restaurant = await restaurantsRepository.GetByIdAsync(request.RestaurantId) ?? throw new NotFoundException(nameof(Restaurant), request.RestaurantId.ToString());

        var dish = mapper.Map<Dish>(request);

        var dishId = await dishesRepository.Create(dish);
        return dishId;
    }
}
