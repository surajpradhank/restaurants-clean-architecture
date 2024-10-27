using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurants.Application.Dishes.Commands.CreateDish;
using Restaurants.Application.Dishes.Commands.DeleteDishes;
using Restaurants.Application.Dishes.Dtos;
using Restaurants.Application.Dishes.Query.GetDishByIdForRestaurant;
using Restaurants.Application.Dishes.Query.GetDishesForRestaurant;

namespace Restaurants.API.Controllers;

[ApiController]
[Route("api/restaurants/{restaurantId}/dishes")]
public class DishesController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishDto>>> GetAllForRestaurant(int restaurantId)
    {
        var dishes = await mediator.Send(new GetDishesForRestaurantQuery(restaurantId));
        return Ok(dishes);
    }

    [HttpGet("{dishId}")]
    public async Task<ActionResult<DishDto>> GetByIdForRestaurant(int restaurantId, int dishId)
    {
        var dish = await mediator.Send(new GetDishByIdForRestaurantQuery(restaurantId, dishId));
        return Ok(dish);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDish(int restaurantId, CreateDishCommand command)
    {
        command.RestaurantId = restaurantId;

        var dishId = await mediator.Send(command);
        return CreatedAtAction(nameof(GetByIdForRestaurant), new { restaurantId, dishId }, null);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteDishesForRestaurant([FromRoute] int restaurantId)
    {
        await mediator.Send(new DeleteDishesForRestaurantCommand(restaurantId));
        return NoContent();
    }
}
