using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents an event that indicates a proposal has been rejected.
/// </summary>
public sealed record ProposalRejected
{
    /// <summary>
    /// The unique identifier for the proposal that has been rejected.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who authored the proposal.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The timestamp indicating when the proposal was rejected.
    /// </summary>
    public DateTime RejectedAt { get; init; }

    /// <summary>
    /// The reason why the proposal was rejected.
    /// </summary>
    public string Reason { get; init; } = string.Empty;
}