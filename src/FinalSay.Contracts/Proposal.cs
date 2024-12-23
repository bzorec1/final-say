using System;
using System.Collections.Generic;

namespace FinalSay.Contracts;

/// <summary>
/// Represents a proposal submitted by a member.
/// </summary>
public record Proposal
{
    /// <summary>
    /// Unique identifier for the proposal.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// Unique identifier of the member who submitted the proposal.
    /// </summary>
    public Guid MemberId { get; init; }

    /// <summary>
    /// Detailed information about the proposal.
    /// </summary>
    public ProposalDetails Details { get; init; } = null!;

    /// <summary>
    /// The date and time when the proposal was submitted.
    /// </summary>
    public DateTime SubmittedAt { get; init; }

    /// <summary>
    /// A collection of members involved in the proposal process.
    /// </summary>
    public IReadOnlyCollection<Member> Members { get; init; } = null!;
}

/// <summary>
/// Represents the core details of a proposal, including its title and description.
/// </summary>
public sealed record ProposalDetails
{
    /// <summary>
    /// The title of the proposal, summarizing its main purpose or goal.
    /// </summary>
    public string Title { get; init; } = null!;

    /// <summary>
    /// A detailed description of the proposal, explaining its content or rationale.
    /// </summary>
    public string Description { get; init; } = null!;
}