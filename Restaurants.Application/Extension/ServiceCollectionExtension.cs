using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Restaurants.Application.Extension;

public static class ServiceCollectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddSCopedServices(services);
    }

    private static void AddSCopedServices(IServiceCollection services)
    {
        var applicationAssembly = typeof(ServiceCollectionExtension).Assembly;

        services.AddMediatR(config => config.RegisterServicesFromAssemblies(applicationAssembly));

        services.AddAutoMapper(applicationAssembly);
        services.AddValidatorsFromAssembly(applicationAssembly)
                .AddFluentValidationAutoValidation();
    }
}
