using Microsoft.Extensions.Caching.Memory;

namespace SOA_Assignment.Common
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;

        public RateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
        {
            _next = next;
            _cache = cache;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress.ToString();
            var cacheKey = $"RateLimit_{ipAddress}";
            var requestCount = _cache.Get<int>(cacheKey);

            if (requestCount >= 10)
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(1)); 

            _cache.Set(cacheKey, requestCount + 1, cacheEntryOptions);

            await _next(context);
        }
    }
}
