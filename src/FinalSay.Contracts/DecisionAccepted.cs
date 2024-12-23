using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents an acceptance of a decision made on a proposal, including the reason for acceptance and the member who accepted it.
/// </summary>
public sealed record DecisionAccepted
{
    /// <summary>
    /// The unique identifier for the accepted decision.
    /// </summary>
    public Guid DecisionId { get; init; }

    /// <summary>
    /// The unique identifier for the proposal related to the accepted decision.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who accepted the decision.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The reason for accepting the decision (optional).
    /// </summary>
    public string? Reason { get; init; }
}