using Microsoft.Extensions.Caching.Memory;

namespace HollyJukeBox.Services;

public class MemoryCashingService(IMemoryCache memoryCache) : IMemoryCashingService
{
    public T Get<T>(string key) where T : class => memoryCache.Get<T>(key);
    public bool Store<T>(string key, T item) where T : class => 
        memoryCache.Set(
            key, 
            item, 
            new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(60)
            }) is T;
}