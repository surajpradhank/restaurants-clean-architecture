using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace Restaurants.API.Controllers.Tests;

[TestClass()]
public class RestaurantsControllerTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact()]
    public async void GetAll_ForValidRequest_Returns200Ok()
    {
        //arrange
        var client = factory.CreateClient();

        //act
        var result = await client.GetAsync("api/restaurants?pageNumber=1&pageSize=10");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);

    }

    [Fact()]
    public async void GetAll_ForInvalidValidRequest_Returns400badRequest()
    {
        //arrange
        var client = factory.CreateClient();

        //act
        var result = await client.GetAsync("api/restaurants");

        //assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);

    }
}