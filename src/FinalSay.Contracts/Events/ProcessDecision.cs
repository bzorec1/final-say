using System;

namespace FinalSay.Contracts.Events;

public sealed record ProcessDecision
{
    public Guid ProposalId { get; init; }

    public Guid AuthorMemberId { get; init; }

    public DecisionOutcome Decision { get; init; }

    public DateTime SubmittedAt { get; init; }
}