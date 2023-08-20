using WorkerApp.Models;

namespace WorkerApp.Repository
{
    public class WorkerRepository : BaseRepository, IWorkerRepository
    {
        public Task AddItems(IEnumerable<CustomItem> items, CancellationToken cancellationToken)
        {
            //TODO: implement bulk insert

            return Task.CompletedTask;
        }
    }
}
