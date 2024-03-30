using pd_base;

namespace strategies;

public class TitForTatStrategy : IStrategy
{
    public string Name => "TitForTat";
    public Character? Character { get; set; }
    public Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player executingPlayer)
    {
        //  returns cooperate on the first turn and then copies the opponent's previous move.
        var previousTurnResult = turnResults.Length == 0 ? Response.Cooperate : executingPlayer == Player.A ? turnResults[^1].B : turnResults[^1].A;
        return previousTurnResult;
    }
}
