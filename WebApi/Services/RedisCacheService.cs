using Newtonsoft.Json;
using StackExchange.Redis;
using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class RedisCacheService : BaseService<RedisCacheService>, ICacheService
    {
        private const string RedisServer = "localhost";
        private const int RedisPort = 6379;
        private string connectionString = $"{RedisServer}:{RedisPort},allowAdmin=true";

        public RedisCacheService(ILogger<RedisCacheService> logger)
            : base(logger) { }

        public async Task<bool> AddItemsToRedis(IEnumerable<CustomItem> items, CancellationToken cancellationToken)
        {
            var connection = await ConnectionMultiplexer.ConnectAsync(connectionString);
            try
            {
                var database = connection.GetDatabase();
                
                var serializedItems = JsonConvert.SerializeObject(items);
                await RetryPolicy.ExecuteAsync(
                    () =>
                        Task.Run(() =>
                        database.StringSetAsync("myItems", serializedItems),
                        cancellationToken
                    )
                );

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex,
                    "Failed to write to Redis items {Ids}", 
                    string.Join(", ", items.Select(x => x.Id))
                );

                return false;
            }
            finally
            {
                await connection.CloseAsync(true);
            }
        }
    }
}
