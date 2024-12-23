using System;

namespace FinalSay.Contracts;

public class DecisionApproved : IProposalEvent
{
    public Guid ProposalId { get; init; }
}