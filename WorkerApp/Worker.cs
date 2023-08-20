using RabbitMQ.Client;
using System.Text;
using RabbitMQ.Client.Events;
using WorkerApp.Models;
using WorkerApp.Repository;

namespace WorkerApp
{
    public class Worker : BackgroundService
    {
        private readonly IWorkerRepository m_Repository;
        private readonly ILogger<Worker> m_Logger;

        public Worker(IWorkerRepository repository, ILogger<Worker> logger)
        {
            m_Repository = repository ?? throw new ArgumentNullException(nameof(repository));
            m_Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitializeConsumerAsync(stoppingToken);

            while (!stoppingToken.IsCancellationRequested)
            {
                m_Logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        private async Task InitializeConsumerAsync(CancellationToken cancellationToken)
        {
            var connectionFactory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };

            using var connection = connectionFactory.CreateConnection();
            using var channel = connection.CreateModel();
            channel.QueueDeclare("demo-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null
            );

            var consumer = new EventingBasicConsumer(channel);
            var newItems = new List<CustomItem>();
            consumer.Received += (_, e) =>
            {
                var body = e.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);

                var itemsFromRedis = Enumerable.Empty<CustomItem>(); //TODO
                newItems.AddRange(itemsFromRedis);
            };

            await m_Repository.AddItems(newItems, cancellationToken);
            newItems.Clear();

            channel.BasicConsume("demo-queue", true, consumer);
            Console.ReadLine();
        }
    }
}