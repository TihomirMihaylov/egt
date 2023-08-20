using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using WebApi.Services.Interfaces;

namespace WebApi.Services
{
    public class RabbitMqService : BaseService<RabbitMqService>, IMessageService
    {
        private readonly ConnectionFactory m_ConnectionFactory;

        public RabbitMqService(ILogger<RabbitMqService> logger)
            : base(logger)
        {
            m_ConnectionFactory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
        }

        public async Task<bool> NotifyConsumers(CancellationToken cancellationToken)
        {
            try
            {
                using var connection = m_ConnectionFactory.CreateConnection();
                using var channel = connection.CreateModel();
                channel.QueueDeclare("demo-queue",
                    durable: true,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null
                );

                var message = new { Name = "Producer", Message = "New items added in Redis" };
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                await RetryPolicy.ExecuteAsync(
                    () => 
                        Task.Run(() => 
                            channel.BasicPublish("", "demo-queue", null, body),
                            cancellationToken
                        )
                );

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Failed to notify consumers");

                return false;
            }
        }
    }
}
