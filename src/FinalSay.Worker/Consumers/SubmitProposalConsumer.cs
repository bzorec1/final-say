using System.Threading.Tasks;
using FinalSay.Worker.Contracts;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FinalSay.Worker.Consumers;

public class SubmitProposalConsumer : IConsumer<SubmitProposal>
{
    private readonly ILogger<SubmitProposalConsumer> _logger;

    public SubmitProposalConsumer(ILogger<SubmitProposalConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<SubmitProposal> context)
    {
        var message = context.Message;

        _logger.LogInformation("Received proposal from {Author} with {MembersCount} members. {Message}", message.Author,
            message.Members.Count, message.ToString());

        await Task.Delay(1000, context.CancellationToken);
    }
}