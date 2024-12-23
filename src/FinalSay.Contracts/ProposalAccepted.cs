using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents an event that indicates a proposal has been accepted.
/// </summary>
public sealed record ProposalAccepted
{
    /// <summary>
    /// The unique identifier for the accepted proposal.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who authored the proposal.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The timestamp indicating when the proposal was accepted.
    /// </summary>
    public DateTime AcceptedAt { get; init; }
}