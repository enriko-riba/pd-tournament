using pd_base;

namespace strategies;
public class TricksterStrategy : IStrategy
{
    public string Name => "Trickster";
    public Character? Character { get; set; }
    public Response MakeDecision(int turnNumber, ReadOnlySpan<TurnResult> turnResults, Player executingPlayer)
    {
        var (defectionsA, defectionsB) = CountDefections(turnResults);
        var difference = executingPlayer == Player.A ? defectionsA - defectionsB : defectionsB - defectionsA;
        var response = difference switch
        {
            >= 0 => Random.Shared.NextDouble() < 0.20 ? Response.Defect : Response.Cooperate,  //  defect in 20% of the cases
            > -3 => Random.Shared.NextDouble() < 0.5 ? Response.Defect : Response.Cooperate,   //  defect in 50% of the cases
            _ => Response.Defect,                                                              //  always defect if opponent has more than 3 defections
        };

        return response;
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
