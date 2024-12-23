namespace FinalSay.Contracts;

/// <summary>
/// Represents the possible outcomes of a decision made by a member.
/// </summary>
public enum DecisionOutcome
{
    /// <summary>
    /// No decision has been made.
    /// </summary>
    None,

    /// <summary>
    /// The member has chosen a neutral stance.
    /// </summary>
    Neutral,

    /// <summary>
    /// The member has approved the proposal.
    /// </summary>
    Approved,

    /// <summary>
    /// The decision is split (indecisive or evenly divided).
    /// </summary>
    Split,

    /// <summary>
    /// The member has rejected the proposal.
    /// </summary>
    Rejected
}