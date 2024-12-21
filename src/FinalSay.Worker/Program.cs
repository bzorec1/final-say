using System;
using System.Reflection;
using System.Threading.Tasks;
using FinalSay.Repository;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace FinalSay.Worker;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) => Host
        .CreateDefaultBuilder(args)
        .ConfigureServices((hostContext, services) =>
        {
            var connectionString = hostContext.Configuration.GetSection("ConnectionStrings:Default").Value;

            services.AddFinalSayRepository(connectionString ?? throw new InvalidOperationException());

            services.AddMassTransit(x =>
            {
                x.AddDelayedMessageScheduler();

                x.SetKebabCaseEndpointNameFormatter();

                x.SetInMemorySagaRepositoryProvider();

                var entryAssembly = Assembly.GetEntryAssembly();

                x.AddConsumers(entryAssembly);
                x.AddSagaStateMachines(entryAssembly);
                x.AddSagas(entryAssembly);
                x.AddActivities(entryAssembly);

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.UseDelayedMessageScheduler();

                    cfg.ConfigureEndpoints(context);
                });
            });
        });
}