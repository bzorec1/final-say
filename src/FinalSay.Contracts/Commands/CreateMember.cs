namespace FinalSay.Contracts.Commands;

/// <summary>
/// Represents a command to create a new member with their name and email address.
/// </summary>
public sealed record CreateMember
{
    /// <summary>
    /// The name of the member to be created.
    /// </summary>
    public string Name { get; init; } = null!;

    /// <summary>
    /// The email address of the member to be created.
    /// </summary>
    public string Email { get; init; } = null!;
}