using WebApi.Models;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class RedisCacheService : BaseService<RedisCacheService>, ICacheService
    {
        //redis connector

        public RedisCacheService(ILogger<RedisCacheService> logger)
            : base(logger) { }

        public async Task<bool> AddItemsToRedis(IEnumerable<CustomItem> items, CancellationToken cancellationToken)
        {
            try
            {
                //call with policy
                //TODO

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
        }
    }
}
