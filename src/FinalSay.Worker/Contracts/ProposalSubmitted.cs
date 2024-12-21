using System;
using System.Collections.Generic;

namespace FinalSay.Worker.Contracts;

public record ProposalSubmitted
{
    public Guid ProposalId { get; init; }

    public string Author { get; init; } = null!;

    public IReadOnlyList<string> Members { get; init; } = null!;
}