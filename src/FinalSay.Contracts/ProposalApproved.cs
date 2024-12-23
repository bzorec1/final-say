using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents an event that indicates a proposal has been approved.
/// </summary>
public sealed record ProposalApproved
{
    /// <summary>
    /// The unique identifier for the approved proposal.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who authored the proposal.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The timestamp indicating when the proposal was approved.
    /// </summary>
    public DateTime ApprovedAt { get; init; }

    /// <summary>
    /// The reason why the proposal was approved.
    /// </summary>
    public string Reason { get; init; } = string.Empty;
}
