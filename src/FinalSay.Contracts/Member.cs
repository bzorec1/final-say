using System;

namespace FinalSay.Contracts;

/// <summary>
/// Represents a member involved in a decision process.
/// </summary>
public sealed record Member
{
    /// <summary>
    /// Unique identifier for the member.
    /// </summary>
    public Guid MemberId { get; init; }

    /// <summary>
    /// Full name of the member.
    /// </summary>
    public string Name { get; init; } = string.Empty; // Ensures non-null default

    /// <summary>
    /// Email address of the member.
    /// </summary>
    public string Email { get; init; } = string.Empty; // Ensures non-null default
}