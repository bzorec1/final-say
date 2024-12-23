using System;
using System.Collections.Generic;

namespace FinalSay.Contracts.Commands;

/// <summary>
/// Represents a request to submit a new proposal for review and decision-making.
/// </summary>
public sealed record SubmitProposal
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