using WorkerApp;
using WorkerApp.DAL;
using WorkerApp.Repository;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        //services.AddDbContext<CustomItemContext>(options =>
        //    options.)

        services.AddSingleton<IWorkerRepository, WorkerRepository>();

        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
