using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Shared;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants
{
    internal class GetAllRestaurantsQueryHandler(ILogger<GetAllRestaurantsQueryHandler> logger,
                                                 IMapper mapper,
                                                 IRestaurantsRepository restaurantsRepository) : IRequestHandler<GetAllRestaurantsQuery, PagesResult<RestaurantDto>>
    {
        public async Task<PagesResult<RestaurantDto>> Handle(GetAllRestaurantsQuery request, CancellationToken cancellationToken)
        {
            logger.LogInformation("Getting all restaurants");
            var (restaurants, totalCount) = await restaurantsRepository.GetAllMatchingAsync(request.SearchPhrase,
                                                                                            request.PageSize,
                                                                                            request.PageNumber,
                                                                                            request.SortBy,
                                                                                            request.SortDirection);

            //manually mapping Restaurant to RestaurantDto class
            //var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity);

            var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

            var result = new PagesResult<RestaurantDto>(restaurantDtos, totalCount, request.PageSize, request.PageNumber);

            return result;
        }
    }
}
