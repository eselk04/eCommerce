using eCommerce.Application.Interfaces.RedisCache;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace eCommerce.Infrastructure.RedisCache;

public class RedisCacheService : IRedisCacheService
{
    private readonly IDatabase database;
    private readonly ConnectionMultiplexer connectionMultiplexer;
    private readonly RedisCacheSettings settings;

    public RedisCacheService(ConnectionMultiplexer connectionMultiplexer , IDatabase database, IOptions<RedisCacheSettings> settings)
    {
      this.settings = settings.Value;
      var opt = ConfigurationOptions.Parse(settings.Value.ConnectionString);
      this.connectionMultiplexer = ConnectionMultiplexer.Connect(opt);
      this.database = connectionMultiplexer.GetDatabase();
       
    }
    public async Task<T> GetAsync<T>(string key)
    {
        var value = await database.StringGetAsync(key);
        if (value.HasValue) return JsonConvert.DeserializeObject<T>(value);
        
        return default;
    }

    public async Task SetAsync<T>(string key, T value, DateTime? expiry = null)
    {
        TimeSpan timeUnitExpiration = expiry.Value - DateTime.Now;
        await database.StringSetAsync(key, JsonConvert.SerializeObject(value) , timeUnitExpiration);
    }
}