using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using Restaurants.Application.Users;

namespace Restaurants.Application.Extension;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {

        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();

        AddScopedServices(services);
        services.AddHttpContextAccessor();
    }

    private static void AddScopedServices(IServiceCollection services)
    {
        services.AddScoped<IUserContext, UserContext>();
    }
}
