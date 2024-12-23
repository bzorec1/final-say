using System;

namespace FinalSay.Contracts.Commands;

/// <summary>
/// Represents a decision submitted for a specific proposal by a member.
/// </summary>
public sealed record SubmitDecision
{
    /// <summary>
    /// The unique identifier of the proposal associated with the decision.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member submitting the decision.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The outcome of the decision, such as Approved, Rejected, or Neutral.
    /// </summary>
    public DecisionOutcome Outcome { get; init; }

    /// <summary>
    /// The date and time when the decision was submitted.
    /// </summary>
    public DateTime SubmittedAt { get; init; }
}