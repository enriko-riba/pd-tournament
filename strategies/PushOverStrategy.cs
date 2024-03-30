using pd_base;

namespace strategies;

public class PushOverStrategy : IStrategy
{
    public string Name => "PushOver";
    public Character? Character { get; set; }
    public Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player player) => Response.Cooperate;
}
