using System;

namespace FinalSay.Contracts.Events;

public sealed record DecisionAccepted
{
    public Guid ProposalId { get; init; }

    public Member Author { get; init; } = null!;

    public bool Decision { get; init; }
}