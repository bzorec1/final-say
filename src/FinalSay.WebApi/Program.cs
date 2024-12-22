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

app.MapPost("/submit/proposal", async (
    IRequestClient<SubmitProposal> requestClient,
    SubmitProposal proposal,
    CancellationToken cancellationToken = default) =>
{
    var response = await requestClient.GetResponse<ProposalSubmitted>(proposal, cancellationToken);

    return Results.Accepted(null, response.Message);
});
app.MapPost("/submit/decision", async (IPublishEndpoint endpoint, SubmitDecision decision) =>
{
    await endpoint.Publish(decision);
    return Results.Accepted();
});

app.Run();