using System;

namespace FinalSay.Worker.Contracts;

public record Member
{
    public Guid MemberId { get; init; }
    
    public string Name { get; init; } = null!;
    
    public string Email { get; init; } = null!;
}