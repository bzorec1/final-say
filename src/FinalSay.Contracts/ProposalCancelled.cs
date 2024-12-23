using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents an event that indicates a proposal has been cancelled.
/// </summary>
public sealed record ProposalCancelled
{
    /// <summary>
    /// The unique identifier for the proposal that has been cancelled.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who authored the proposal.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The timestamp indicating when the proposal was cancelled.
    /// </summary>
    public DateTime CancelledAt { get; init; }

    /// <summary>
    /// The reason why the proposal was cancelled.
    /// </summary>
    public string Reason { get; init; } = string.Empty;
}