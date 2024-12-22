using System;

namespace FinalSay.Contracts;

public record Decisions
{
    public string Member { get; init; } = null!;

    public bool Approved { get; init; }

    public DateTime DecidedAt { get; init; }
}