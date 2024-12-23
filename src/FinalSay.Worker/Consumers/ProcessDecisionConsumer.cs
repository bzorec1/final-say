using System.Threading.Tasks;
using FinalSay.Contracts.Events;
using FinalSay.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FinalSay.Worker.Consumers;

public class ProcessDecisionConsumer : IConsumer<ProcessDecision>
{
    private readonly FinalSayDbContext _dbContext;
    private readonly ILogger<ProcessDecisionConsumer> _logger;

    public ProcessDecisionConsumer(ILogger<ProcessDecisionConsumer> logger, FinalSayDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<ProcessDecision> context)
    {
        var message = context.Message;

        _logger.LogInformation("Received decision from {Author} for proposal {Proposal}. {Message}",
            message.AuthorMemberId,
            message.ProposalId,
            message.ToString());

        await Task.Delay(3000);

        _logger.LogInformation("Proposal {ProposalId} submitted", message.ProposalId);
    }
}