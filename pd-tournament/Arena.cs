using pd_base;
using System.Collections.Concurrent;

namespace pd_tournament;

/// <summary>
/// Helper class to simulate a tournament between a set of strategies with the given number of rounds per match.
/// </summary>
/// <param name="baseNumberOfTurns">Approximate number of turns per match</param>
/// <param name="turnNumberVariationPercentage">variation percentage in range [0,1]. Variations are necessary so 
/// that the real number of turns stays unknown to strategies - knowing the exact number of turns would allow for
/// special endgame strategies causing only defections to be optimal</param>
/// <param name="strategies">competing strategies</param>
public class Arena(int baseNumberOfTurns, double turnNumberVariationPercentage, IEnumerable<IStrategy> strategies)
{
    /// <summary>
    /// Runs all tournament simulations where each strategy plays against all other strategies and itself.
    /// </summary>
    /// <returns></returns>
    public IEnumerable<Match> RunAll()
    {
        var results = new ConcurrentBag<Match>();
        var totalStrategies = strategies.Count();
        var variation = (int)(baseNumberOfTurns * turnNumberVariationPercentage);
        var numberOfTurns = baseNumberOfTurns + new Random().Next(-variation, variation);
        Parallel.For(0, totalStrategies, i =>
        {
            Parallel.For(i, totalStrategies, j =>
            {
                var strategyA = strategies.ElementAt(i);
                var strategyB = strategies.ElementAt(j);
                var result = RunMatch(numberOfTurns, strategyA, strategyB);
                results.Add(result);
            });
        });
        return results;
    }

    private static Match RunMatch(int numberOfTurns, IStrategy strategyA, IStrategy strategyB)
    {
        strategyA.Character ??= Character.Nice;
        strategyB.Character ??= Character.Nice;
        var hasDefected = false;

        var turnResults = new TurnResult[numberOfTurns];
        for (int turn = 0; turn < numberOfTurns; turn++)
        {
            var responseA = strategyA.MakeDecision(turn, turnResults.AsSpan(0, turn), Player.A);
            var responseB = strategyB.MakeDecision(turn, turnResults.AsSpan(0, turn), Player.B);
            if(!hasDefected && (responseA == Response.Defect || responseB == Response.Defect))
            {
                hasDefected = true;
                if(responseA == Response.Defect)
                {
                    strategyA.Character = Character.Nasty;
                }
                if(responseB == Response.Defect)
                {
                    strategyB.Character = Character.Nasty;
                }
            }
            turnResults[turn] = new TurnResult(turn, responseA, responseB);
        }

        var (scoreA, scoreB) = CalcScores(turnResults);
        return new Match(strategyA, strategyB, turnResults, scoreA, scoreB);
    }

    private static (int scoreA, int scoreB) CalcScores(IEnumerable<TurnResult> turnResults)
    {
        // if both A nd B cooperate, they both get 3 points,
        // if A cooperates and B defects, A gets 0 points and B gets 5 points,
        // if A defects and B cooperates, A gets 5 points and B gets 0 points,
        // if both defect, they both get 1 point.
        var scoreA = turnResults.Sum(r => r.A == Response.Cooperate &&
            r.B == Response.Cooperate ? 3 :
            r.A == Response.Cooperate && r.B == Response.Defect ? 0 :
            r.A == Response.Defect && r.B == Response.Cooperate ? 5 :
            r.A == Response.Defect && r.B == Response.Defect ? 1 : 0);

        var scoreB = turnResults.Sum(r => r.B == Response.Cooperate &&
            r.A == Response.Cooperate ? 3 :
            r.B == Response.Cooperate && r.A == Response.Defect ? 0 :
            r.B == Response.Defect && r.A == Response.Cooperate ? 5 :
            r.B == Response.Defect && r.A == Response.Defect ? 1 : 0);

        return (scoreA, scoreB);
    }
}

public record Match(IStrategy StrategyA, IStrategy StrategyB, TurnResult[] TurnResults, int ScoreA, int ScoreB);
