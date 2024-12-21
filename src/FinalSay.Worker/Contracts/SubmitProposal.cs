using System;
using System.Collections.Generic;

namespace FinalSay.Worker.Contracts;

public record SubmitProposal
{
    public Guid ProposalId { get; init; }

    public ProposalContent Content { get; init; } = null!;

    public string Author { get; init; } = null!;
    
    public DateTime SubmittedAt { get; init; }

    public IReadOnlyList<Member> Members { get; init; } = null!;
}