
using System.Diagnostics;

namespace Restaurants.API.Middleware;

public class RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWatch.Stop();

        if(stopWatch.ElapsedMilliseconds/1000 > 4)
        {
            logger.LogInformation("Request {verb} at {Path} took {Time} s",
                                   context.Request.Method,
                                   context.Request.Path,
                                   stopWatch.ElapsedMilliseconds/1000);
        }
    }
}
