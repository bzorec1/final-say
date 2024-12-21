using System;

namespace FinalSay.Worker.Contracts;

public record SubmitDecision
{
    public Guid ProposalId { get; init; }

    public string Author { get; init; } = null!;

    public string Decision { get; init; } = null!;
    
    public DateTime SubmittedAt { get; init; }
}