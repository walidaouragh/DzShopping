using System;
using System.Threading.Tasks;

namespace DzShopping.Infrastructure.Services.ResponseCacheService
{
    public interface IResponseCacheService
    {
        Task CacheResponseAsync(string cacheKey, object response, TimeSpan timeToLive);

        Task<string> GetCacheResponseAsync(string cacheKey);
    }
}
