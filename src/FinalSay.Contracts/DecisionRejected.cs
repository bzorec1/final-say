using System;

namespace FinalSay.Contracts;

public class DecisionRejected(Guid ProposalId) : IProposalEvent
{
    public Guid ProposalId { get; init; }
}