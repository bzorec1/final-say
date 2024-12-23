using System;

namespace FinalSay.Contracts.Events;

/// <summary>
/// Represents an event that indicates a new member has been successfully created.
/// </summary>
public sealed record MemberCreated
{
    /// <summary>
    /// The unique identifier for the newly created member.
    /// </summary>
    public Guid MemberId { get; init; }
}