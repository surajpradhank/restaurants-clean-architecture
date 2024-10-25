using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Query.GetRestaurantById;

public class GetRestaurantByIdQueryHandler(ILogger<GetRestaurantByIdQueryHandler> logger,
                                           IMapper mapper,
                                           IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetRestaurantByIdQuery, RestaurantDto?>
{
    public async Task<RestaurantDto?> Handle(GetRestaurantByIdQuery request, CancellationToken cancellationToken)
    {
        logger.LogInformation($"Getting restaurant by id {request.Id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

        //manually mapping Restaurant to RestaurantDto class
        //var restaurantDtos = RestaurantDto.FromEntity(restaurant);

        var restaurantDtos = mapper.Map<RestaurantDto?>(restaurant);

        return restaurantDtos;
    }
}
