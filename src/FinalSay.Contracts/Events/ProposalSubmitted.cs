using System;
using System.Collections.Generic;

namespace FinalSay.Contracts.Events;

public sealed record ProposalSubmitted
{
    public Guid ProposalId { get; init; }

    public Member Author { get; init; } = null!;

    public ProposalDetails Details { get; init; } = null!;
    
    public IReadOnlyList<Member> Members { get; init; } = null!;
}