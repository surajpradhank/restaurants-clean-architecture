using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(IRestaurantsRepository restaurantsRepository,
                                   ILogger<IRestaurantsRepository> logger,
                                   IMapper mapper) : IRestaurantsService
{
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();

        //manually mapping Restaurant to RestaurantDto class
        //var restaurantDtos = restaurants.Select(RestaurantDto.FromEntity);

        var restaurantDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);

        return restaurantDtos!;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant by id {id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);

        //manually mapping Restaurant to RestaurantDto class
        //var restaurantDtos = RestaurantDto.FromEntity(restaurant);

        var restaurantDtos = mapper.Map<RestaurantDto?>(restaurant);

        return restaurantDtos;
    }


    public async Task<int> Create(CreateRestaurantDto dto)
    {
        logger.LogInformation("Creating new restaurant");

        var restaurant = mapper.Map<Restaurant>(dto);
        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }
}
