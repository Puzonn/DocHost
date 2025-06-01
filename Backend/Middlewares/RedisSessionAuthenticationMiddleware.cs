using Microsoft.Extensions.Caching.Distributed;

namespace DocHost.Middlewares;

public class RedisSessionAuthenticationMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, IDistributedCache cache)
    {
        /* AuthenticationHandler will take care of User Identity */
        await next(context);
    }
}