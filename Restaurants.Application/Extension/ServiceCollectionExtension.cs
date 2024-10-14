using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Restaurants;

namespace Restaurants.Application.Extension;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddSCopedServices(services);
    }

    private static void AddSCopedServices(IServiceCollection services)
    {
        var appplicationAssembly = typeof(ServiceCollectionExtension).Assembly;

        services.AddScoped<IRestaurantsService, RestaurantsService>();
        services.AddAutoMapper(appplicationAssembly);
        services.AddValidatorsFromAssembly(appplicationAssembly)
                .AddFluentValidationAutoValidation();
    }
}
