namespace FinalSay.Contracts;

/// <summary>
/// Represents the core details of a proposal, including its title and description.
/// </summary>
public sealed record ProposalDetails
{
    /// <summary>
    /// The title of the proposal, summarizing its main purpose or goal.
    /// </summary>
    public string Title { get; init; } = null!;

    /// <summary>
    /// A detailed description of the proposal, explaining its content or rationale.
    /// </summary>
    public string Description { get; init; } = null!;
}