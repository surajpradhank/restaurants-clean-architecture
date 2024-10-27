using Restaurants.Domain.Entities;
using Restaurants.Domain.Repository;
using Restaurants.Infrastructure.Persistence;

namespace Restaurants.Infrastructure.Repositories;

public class DishesRepository(RestaurantsDbContext dbContext) : IDishesRepository
{
    public async Task<int> Create(Dish dish)
    {
        dbContext.Dishes.Add(dish);
        await dbContext.SaveChangesAsync();
        return dish.Id;
    }

    public async Task Delete(IEnumerable<Dish> entities)
    {
        dbContext.Dishes.RemoveRange(entities);
        await dbContext.SaveChangesAsync();
    }
}
