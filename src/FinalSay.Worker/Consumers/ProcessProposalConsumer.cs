using System.Linq;
using System.Threading.Tasks;
using FinalSay.Contracts;
using FinalSay.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FinalSay.Worker.Consumers;

public class ProcessProposalConsumer : IConsumer<ProcessProposal>
{
    private readonly FinalSayDbContext _dbContext;
    private readonly ILogger<ProcessProposalConsumer> _logger;

    public ProcessProposalConsumer(ILogger<ProcessProposalConsumer> logger, FinalSayDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<ProcessProposal> context)
    {
        var message = context.Message;

        _logger.LogInformation("Received proposal from {Author} with {MembersCount} members. {Message}",
            message.Author,
            message.Members.Count,
            message.ToString());

        await _dbContext.Set<Proposal>().AddAsync(new Proposal(), context.CancellationToken);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

        if (context.RequestId.HasValue && context.ResponseAddress != null)
        {
            await context.RespondAsync<ProposalAccepted>(new
            {
                message.ProposalId,
                message.Author,
                Members = message.Members.Select(x => x.Email).ToList()
            });
        }

        _logger.LogInformation("Proposal {ProposalId} submitted", message.ProposalId);
    }
}

public class Proposal
{
}