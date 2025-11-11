using Microsoft.Extensions.Caching.Distributed;

namespace Evently.Common.Infrastructure.Caching;
public static class CacheOptions
{
    public static DistributedCacheEntryOptions DefaultExpiration =>
        new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        };


    public static DistributedCacheEntryOptions Create(TimeSpan? timeSpan ) =>
       
        timeSpan is not null
        ?
        new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        }
        : DefaultExpiration;



}
