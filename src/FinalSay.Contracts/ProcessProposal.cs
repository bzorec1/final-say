using System;
using System.Collections.Generic;

namespace FinalSay.Contracts;

/// <summary>
/// Represents a command to process a proposal, containing the proposal details and the associated members for review and decision.
/// </summary>
public sealed record ProcessProposal
{
    /// <summary>
    /// The unique identifier for the proposal.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who authored the proposal.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The details of the proposal, including title and description.
    /// </summary>
    public ProposalDetails Details { get; init; } = null!;

    /// <summary>
    /// The date and time when the proposal was submitted.
    /// </summary>
    public DateTime SubmittedAt { get; init; }

    /// <summary>
    /// A list of unique identifiers representing the members invited to review and decide on the proposal.
    /// </summary>
    public IReadOnlyList<Guid> Members { get; init; } = null!;
}