using FinalSay.Contracts;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.UseDelayedMessageScheduler();

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/submit/proposal", async (IPublishEndpoint endpoint, SubmitProposal proposal) =>
{
    await endpoint.Publish(proposal);
    return Results.Accepted();
});
app.MapPost("/submit/decision", async (IPublishEndpoint endpoint, SubmitDecision decision) =>
{
    await endpoint.Publish(decision);
    return Results.Accepted();
});

app.Run();