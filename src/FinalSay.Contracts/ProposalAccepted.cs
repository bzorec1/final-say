using System;

namespace FinalSay.Contracts;

public class ProposalAccepted(Guid ProposalId) : IProposalEvent
{
    public Guid ProposalId { get; init; }
}

public interface IProposalEvent
{
    public Guid ProposalId { get; init; }
}