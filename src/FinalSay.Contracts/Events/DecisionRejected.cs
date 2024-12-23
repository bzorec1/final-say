using System;

namespace FinalSay.Contracts.Events;

public sealed record DecisionRejected
{
    public Guid ProposalId { get; init; }

    public Guid AuthorMemberId { get; init; }

    public DecisionOutcome Decision { get; init; }

    public string? Reason { get; init; }
}