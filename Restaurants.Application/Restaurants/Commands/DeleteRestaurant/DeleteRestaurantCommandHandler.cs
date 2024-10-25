using MediatR;
using Microsoft.Extensions.Logging;
using Restaurants.Domain.Repository;

namespace Restaurants.Application.Restaurants.Commands.DeleteRestaurant
{
    internal class DeleteRestaurantCommandHandler(ILogger<DeleteRestaurantCommand> logger,
                                                  IRestaurantsRepository restaurantsRepository) : IRequestHandler<DeleteRestaurantCommand, bool>
    {
        public async Task<bool> Handle(DeleteRestaurantCommand request, CancellationToken cancellationToken)
        {
            logger.LogInformation($"Deleting restaurant with id : {request.Id}");

            var restaurant = await restaurantsRepository.GetByIdAsync(request.Id);

            if (restaurant == null)
                return false;

            await restaurantsRepository.Delete(restaurant);

            return true;
        }
    }
}
