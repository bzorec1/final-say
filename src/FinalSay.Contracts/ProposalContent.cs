namespace FinalSay.Contracts;

public record ProposalContent
{
    public string Title { get; init; } = null!;

    public string Description { get; init; } = null!;
}