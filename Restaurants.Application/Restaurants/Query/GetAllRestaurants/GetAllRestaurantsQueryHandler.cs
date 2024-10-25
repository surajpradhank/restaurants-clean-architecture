using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants
{
    internal class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
                                                 IMapper mapper,
                                                 IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, IEnumerable<RestaurantDto>>
    {
        public async Task<IEnumerable<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var restaurants = await restaurantsRepository.GetAllAsync();

            //manually mapping Restaurant to RestaurantDto class
            //var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity);

            var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            return restaurantDtos!;
        }
    }
}
