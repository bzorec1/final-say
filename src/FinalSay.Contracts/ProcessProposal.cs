using System;
using System.Collections.Generic;

namespace FinalSay.Contracts;

public record ProcessProposal(Guid ProposalId, ProposalContent Content, string Author, DateTime SubmittedAt, IReadOnlyList<Member> Members);