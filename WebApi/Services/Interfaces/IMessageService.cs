namespace WebApi.Services.Interfaces
{
    public interface IMessageService
    {
        Task<bool> NotifyConsumers(CancellationToken cancellationToken);
    }
}
