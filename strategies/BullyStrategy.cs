using pd_base;

namespace strategies;

public class BullyStrategy : IStrategy
{
    public string Name => "Bully";
    public Character? Character { get; set; }
    public Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player executingPlayer) => Response.Defect;
}
