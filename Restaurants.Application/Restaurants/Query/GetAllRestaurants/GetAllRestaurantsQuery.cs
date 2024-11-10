using MediatR;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Application.Shared;
using Restaurants.Domain.Constants;

namespace Restaurants.Application.Restaurants.Query.GetAllRestaurants;

public class GetAllRestaurantsQuery : IRequest<PagesResult<RestaurantDto>>
{
    public string? SearchPhrase { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? SortBy { get; set; }
    public SortDirection SortDirection { get; set; }
}
