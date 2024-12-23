using System;
using System.Collections.Generic;

namespace FinalSay.Contracts.Events;

public sealed record ProcessProposal
{
    public Guid ProposalId { get; init; }

    public ProposalDetails Details { get; init; } = null!;

    public Guid AuthorMemberId { get; init; }

    public DateTime SubmittedAt { get; init; }

    public IReadOnlyList<Guid> Members { get; init; } = null!;
}