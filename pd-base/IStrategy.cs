namespace pd_base;

public interface IStrategy
{
    string Name { get; }

    /// <summary>
    /// Strategy's character - calculated by the tournament.
    /// </summary>
    public Character? Character { get; set; }

    Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player executingPlayer);
}

/// <summary>
/// Defines which player is executing the strategy.
/// </summary>
public enum Player
{
    A,
    B
}

/// <summary>
/// Defines the possible responses for a player in a turn.
/// </summary>
public enum  Response
{
    Cooperate,
    Defect
}

/// <summary>
/// Main character of the strategy.
/// </summary>
public enum Character
{
    Nice,
    Nasty
}

/// <summary>
/// Represents the result of a single turn.
/// </summary>
/// <param name="TurnNumber"></param>
/// <param name="A"></param>
/// <param name="B"></param>
public record TurnResult(int TurnNumber, Response A, Response B);