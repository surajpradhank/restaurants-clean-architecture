using Restaurants.Domain.Entities;

namespace Restaurants.Domain.Repository;

public interface IDishesRepository
{
    Task<int> Create(Dish dish);
    Task Delete(IEnumerable<Dish> entities);
}
