using System;
using System.Threading.Tasks;
using FinalSay.Repository;
using FinalSay.Worker.Consumers;
using Microsoft.Extensions.Hosting;
using MassTransit;

namespace FinalSay.Worker;

public static class Program
{
    public static async Task Main(string[] args)
    {
        await CreateHostBuilder(args).Build().RunAsync();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        return Host
            .CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                var connectionString = hostContext.Configuration.GetSection("ConnectionStrings:Default").Value;

                services.AddFinalSayRepository(connectionString ?? throw new InvalidOperationException());

                services.AddMassTransit(x =>
                {
                    x.AddDelayedMessageScheduler();
                    x.SetKebabCaseEndpointNameFormatter();
                    
                    x.AddConsumer<ProcessProposalConsumer>();
                    
                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.UseDelayedMessageScheduler();

                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
    }
}