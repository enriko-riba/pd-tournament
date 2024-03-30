using pd_base;

namespace strategies;

public class RandomStrategy : IStrategy
{
    public string Name => "Random";
    public Character? Character { get; set; }
    public Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player executingPlayer) => Random.Shared.NextDouble() < 0.5 ? Response.Cooperate : Response.Defect;
}
