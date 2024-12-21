namespace FinalSay.Worker.Contracts;

public record DecisionDetails
{
    public string Decision { get; init; } = null!;

    public string? Reason { get; init; }
}