using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents a command to process a decision on a proposal, including the decision details and the member who made the decision.
/// </summary>
public sealed record ProcessDecision
{
    /// <summary>
    /// The unique identifier for the decision.
    /// </summary>
    public Guid DecisionId { get; init; }

    /// <summary>
    /// The unique identifier for the proposal being decided upon.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who made the decision.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The outcome of the decision made on the proposal.
    /// </summary>
    public DecisionOutcome Decision { get; init; }

    /// <summary>
    /// The date and time when the decision was submitted.
    /// </summary>
    public DateTime SubmittedAt { get; init; }
}