using pd_base;

namespace strategies;

public class GrimTriggerStrategy : IStrategy
{
    public string Name => "GrimTrigger";
    public Character? Character { get; set; }
    public Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player executingPlayer)
    {
        //  cooperate until the opponent defects, then start retaliating forever.
        return HasOpponentDefected(turnResults, executingPlayer) ? Response.Defect : Response.Cooperate;
    }

    private static bool HasOpponentDefected(ReadOnlySpan<TurnResult> turnResults, Player executingPlayer)
    {
        foreach (var turnResult in turnResults)
        {
            if (executingPlayer == Player.A && turnResult.B == Response.Defect)
            {
                return true;
            }
            if (executingPlayer == Player.B && turnResult.A == Response.Defect)
            {
                return true;
            }
        }
        return false;
    }
}
