using System.Buffers;
using System.Text.Json;
using Evently.Common.Application.Caching;
using Microsoft.Extensions.Caching.Distributed;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Evently.Common.Infrastructure.Caching;
internal sealed class CacheService(
    IDistributedCache _distributedCache) : ICacheService
{

    public async Task<T> GetAsync<T>(string key, CancellationToken cancellationToken = default)
    {
        byte[]? bytes = await _distributedCache.GetAsync(key, cancellationToken);
            
        return bytes is null 
            ? default 
            : System.Text.Json.JsonSerializer.Deserialize<T>(bytes);
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _distributedCache.RemoveAsync(key, cancellationToken);
    }

    public Task SetAsync<T>(
        string key, 
        T value,
        TimeSpan? absoluteExpirationRelativeToNow = null, 
        CancellationToken cancellationToken = default)
    {

        byte[] bytes  = Serializer(value);

        return  _distributedCache.SetAsync(
            key,
            bytes,
            CacheOptions.Create(absoluteExpirationRelativeToNow),
            cancellationToken);
    }

    private static byte[] Serializer<T>(T value)
    {
        var arrayBufferWriter = new ArrayBufferWriter<byte>();

        using var utf8JsonWriter = new Utf8JsonWriter(arrayBufferWriter);

       System.Text.Json.JsonSerializer.Serialize(utf8JsonWriter, value);

        return arrayBufferWriter.WrittenSpan.ToArray();

    }

}
