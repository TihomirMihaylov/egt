using WorkerApp.Models;

namespace WorkerApp.Repository
{
    public interface IWorkerRepository
    {
        Task AddItems(IEnumerable<CustomItem> items, CancellationToken cancellationToken);
    }
}
