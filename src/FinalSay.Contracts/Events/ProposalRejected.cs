using System;

namespace FinalSay.Contracts.Events;

public sealed record ProposalRejected
{
    public Guid ProposalId { get; init; }

    public Member Author { get; init; } = null!;

    public DecisionDetails Details { get; init; } = null!;

    public string? Reason { get; init; }
}