using System;
using System.Collections.Generic;

namespace FinalSay.Contracts;

/// <summary>
/// Represents a request for the current state and decisions of a proposal.
/// </summary>
public sealed record FinalSayStateRequested
{
    /// <summary>
    /// The unique identifier for the proposal being requested.
    /// </summary>
    public Guid ProposalId { get; set; }

    /// <summary>
    /// The current state of the proposal.
    /// </summary>
    public int CurrentState { get; set; }

    /// <summary>
    /// A list of decisions made by members regarding the proposal.
    /// </summary>
    public List<MemberDecision> Decisions { get; set; } = new List<MemberDecision>();

    /// <summary>
    /// The date and time when the request for the proposal state was made.
    /// </summary>
    public DateTime SubmittedAt { get; set; }

    /// <summary>
    /// The date and time when the decision on the proposal was made (optional).
    /// </summary>
    public DateTime? DecidedAt { get; set; }
}