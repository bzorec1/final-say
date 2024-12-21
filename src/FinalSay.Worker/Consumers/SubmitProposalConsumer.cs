using System.Linq;
using System.Threading.Tasks;
using FinalSay.Contracts;
using FinalSay.Repository;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace FinalSay.Worker.Consumers;

public class SubmitProposalConsumer : IConsumer<SubmitProposal>
{
    private readonly FinalSayDbContext _dbContext;
    private readonly ILogger<SubmitProposalConsumer> _logger;

    public SubmitProposalConsumer(ILogger<SubmitProposalConsumer> logger, FinalSayDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public async Task Consume(ConsumeContext<SubmitProposal> context)
    {
        var message = context.Message;

        _logger.LogInformation("Received proposal from {Author} with {MembersCount} members. {Message}",
            message.Author,
            message.Members.Count,
            message.ToString());

        await _dbContext.Set<SubmitProposal>().AddAsync(message, context.CancellationToken);
        await _dbContext.SaveChangesAsync(context.CancellationToken);

        await context.Publish<ProposalSubmitted>(new
        {
            message.ProposalId,
            message.Author,
            Members = message.Members.Select(x => x.Email).ToList()
        });
    }
}