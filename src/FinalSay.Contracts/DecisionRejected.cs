using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents a rejection of a decision made on a proposal, including the reason for rejection and the member who rejected it.
/// </summary>
public sealed record DecisionRejected
{
    /// <summary>
    /// The unique identifier for the rejected decision.
    /// </summary>
    public Guid DecisionId { get; init; }

    /// <summary>
    /// The unique identifier for the proposal related to the rejected decision.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who rejected the decision.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The reason for rejecting the decision (optional).
    /// </summary>
    public string? Reason { get; init; }
}