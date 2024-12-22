﻿using System;

namespace FinalSay.Contracts;

public record SubmitDecision
{
    public Guid ProposalId { get; init; }

    public string Author { get; init; } = null!;

    public bool Decision { get; init; }
    
    public DateTime SubmittedAt { get; init; }
}