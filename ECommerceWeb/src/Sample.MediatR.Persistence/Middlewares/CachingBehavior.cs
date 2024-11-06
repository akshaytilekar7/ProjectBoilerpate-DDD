using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace Sample.MediatR.Persistence.Middlewares;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IMemoryCache _cacheService;

    public CachingBehavior(IMemoryCache cacheService)
    {
        _cacheService = cacheService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cacheKey = request.ToString();  // Generate a cache key based on the request details

        if (_cacheService.TryGetValue(cacheKey, out TResponse cachedResponse))
            return cachedResponse;

        var response = await next(); // Proceed to the next handler (DB call)

        // Cache the result for 1 minute
        _cacheService.Set(cacheKey, response, TimeSpan.FromMinutes(1));

        return response;
    }
}
