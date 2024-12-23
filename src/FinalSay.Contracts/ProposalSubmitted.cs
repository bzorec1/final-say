using System;
using System.Collections.Generic;

namespace FinalSay.Contracts;

/// <summary>
/// Represents an event that indicates a proposal has been successfully submitted.
/// </summary>
public sealed record ProposalSubmitted
{
    /// <summary>
    /// The unique identifier for the proposal that has been submitted.
    /// </summary>
    public Guid ProposalId { get; init; }

    /// <summary>
    /// The unique identifier of the member who authored the proposal.
    /// </summary>
    public Guid AuthorMemberId { get; init; }

    /// <summary>
    /// The details of the proposal, such as title and description.
    /// </summary>
    public ProposalDetails Details { get; init; } = null!;

    /// <summary>
    /// A collection of decisions related to the proposal, each decision made by a member.
    /// </summary>
    public IReadOnlyList<MemberDecision> Decisions { get; init; } = null!;
}