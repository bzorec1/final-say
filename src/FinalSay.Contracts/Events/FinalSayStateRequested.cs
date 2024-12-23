using System;
using System.Collections.Generic;

namespace FinalSay.Contracts.Events;

public sealed record FinalSayStateRequested
{
    public Guid ProposalId { get; set; }

    public int CurrentState { get; set; }

    public List<MemberDecision> Decisions { get; set; } = [];

    public DateTime SubmittedAt { get; set; }

    public DateTime? DecidedAt { get; set; }
}