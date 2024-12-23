using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents a decision made by a member on a proposal.
/// </summary>
public sealed record MemberDecision
{
    /// <summary>
    /// Unique identifier for the member decision.
    /// </summary>
    public Guid MemberDecisionId { get; init; }

    /// <summary>
    /// Unique identifier for the member who made the decision.
    /// </summary>
    public Guid MemberId { get; init; }

    /// <summary>
    /// Unique identifier for the proposal being decided on.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The date and time when the decision was submitted.
    /// </summary>
    public DateTime SubmittedAt { get; init; }

    /// <summary>
    /// The outcome of the decision.
    /// </summary>
    public DecisionOutcome Outcome { get; init; }
}