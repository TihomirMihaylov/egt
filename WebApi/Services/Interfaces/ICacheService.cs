using WebApi.Models;

namespace WebApi.Services.Interfaces
{
    public interface ICacheService
    {
        Task<bool> AddItemsToRedis(IEnumerable<CustomItem> items, CancellationToken cancellationToken);
    }
}
