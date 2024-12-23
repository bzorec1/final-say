using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents the possible outcomes of a decision made by a member.
/// </summary>
public enum DecisionOutcome
{
    /// <summary>
    /// No decision has been made.
    /// </summary>
    None,

    /// <summary>
    /// The member has chosen a neutral stance.
    /// </summary>
    Neutral,

    /// <summary>
    /// The member has approved the proposal.
    /// </summary>
    Approved,

    /// <summary>
    /// The decision is split (indecisive or evenly divided).
    /// </summary>
    Split,

    /// <summary>
    /// The member has rejected the proposal.
    /// </summary>
    Rejected
}

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

public sealed record DecisionDetails
{
    public bool Decision { get; init; }

    public string? Reason { get; init; }
}