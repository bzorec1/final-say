using System;
using System.Collections.Generic;

namespace FinalSay.Contracts;

public record ProcessProposal
{
    public ProcessProposal(SubmitProposal message)
    {
        ProposalId = message.ProposalId;
        Content = message.Content;
        Author = message.Author;
        SubmittedAt = message.SubmittedAt;
        Members = message.Members;
    }

    public Guid ProposalId { get; init; }

    public ProposalContent Content { get; init; }

    public string Author { get; init; }

    public DateTime SubmittedAt { get; init; }

    public IReadOnlyList<Member> Members { get; init; }
}