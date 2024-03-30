using pd_base;

namespace strategies;

public class NiceGuyStrategy : IStrategy
{
    public string Name => "NiceGuy";
    public Character? Character { get; set; }
    public Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player executingPlayer)
    {
        var (defectionsA, defectionsB) = CountDefections(turnResults);
        var difference = executingPlayer == Player.A ? defectionsA - defectionsB : defectionsB - defectionsA;
        return difference < -3 ? Response.Defect : Response.Cooperate;
    }

    private static (int defectionsA, int defectionsB) CountDefections(ReadOnlySpan<TurnResult> turnResults)
    {
        int countA = 0;
        int countB = 0;
        foreach (var turnResult in turnResults)
        {
            if (turnResult.A == Response.Defect)
            {
                countA++;
            }
            if ((turnResult.B == Response.Defect))
            {
                countB++;
            }
        }
        return (countA, countB);
    }
}