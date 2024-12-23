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