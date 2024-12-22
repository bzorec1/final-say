using System;

namespace FinalSay.Contracts;

public class ProcessDecision
{
    public Guid ProposalId { get; init; }

    public string Member { get; init; } = null!;

    public bool Decision { get; init; }
}