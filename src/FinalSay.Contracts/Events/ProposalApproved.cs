using System;

namespace FinalSay.Contracts.Events;

public record ProposalApproved
{
    public Guid ProposalId { get; init; }
    
    public Member Author { get; init; } = null!;
    
    public DecisionDetails Details { get; init; } = null!;
}