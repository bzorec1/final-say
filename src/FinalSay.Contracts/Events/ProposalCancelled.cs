using System;

namespace FinalSay.Contracts.Events;

public sealed record ProposalCancelled
{
    public Guid ProposalId { get; init; }

    public Guid AuthorMemberId { get; init; }

    public ProposalDetails Details { get; init; } = null!;

    public string? Error { get; init; }
}