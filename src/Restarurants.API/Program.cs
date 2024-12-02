using Restaurants.API.Extensions;
using Restaurants.API.Middleware;
using Restaurants.Application.Extension;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extension;
using Restaurants.Infrastructure.Seeders;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.AddPresentation();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

var app = builder.Build();

var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<RequestTimeMiddleware>();
app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("api/Identity").WithTags("Identity").MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program();