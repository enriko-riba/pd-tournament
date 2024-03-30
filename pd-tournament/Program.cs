// See https://aka.ms/new-console-template for more information
using pd_base;
using pd_tournament;
using strategies;

var arena = new Arena(
    10000, 125,
    [
        new BullyStrategy(),
        new RandomStrategy(),
        new NiceGuyStrategy(),
        new TitForTatStrategy(),
        new PushOverStrategy(),
        new TricksterStrategy(),
        new GrimTriggerStrategy(),
    ]);

var matches = arena.RunAll();
var turns = matches.First().TurnResults.Length;
var maxPoints = turns * 5;
var maxCooperativePoints = turns * 3;

Console.WriteLine($"Total turns: {turns}");
Console.WriteLine($"Max defecting points: {maxPoints}");
Console.WriteLine($"Max cooperative points: {maxCooperativePoints}");
Console.WriteLine(new string('=', 80));
Console.WriteLine($"strategy name                 :   player A       |   player B");
Console.WriteLine(new string('-', 80));
var ranking = new Dictionary<IStrategy, int>();

foreach (var match in matches.OrderBy(x => x.StrategyA.Name).ThenBy(x => x.StrategyB.Name))
{
    //  bookkeeping
    if(!ranking.ContainsKey(match.StrategyA))
        ranking[match.StrategyA] = 0;
    if (!ranking.ContainsKey(match.StrategyB))
        ranking[match.StrategyB] = 0;

    ranking[match.StrategyA] += match.ScoreA;
    ranking[match.StrategyB] += match.ScoreB;

    //  dump match results
    var names = $"{match.StrategyA.Name} vs {match.StrategyB.Name}";
    var scoreA = FormatScore(match.ScoreA, maxPoints);
    var scoreB = FormatScore(match.ScoreB, maxPoints);
    Console.WriteLine($"{names,-30}: {scoreA} | {scoreB}");
}

Console.WriteLine(new string('=', 80));

var totalMaxPoints = maxPoints * matches.Count();
var rankList = ranking.OrderByDescending(kvp => kvp.Value);
var rank = 1;
foreach(var (strategy, score) in rankList)
{
    var numberOfMatches = matches.Count(m => m.StrategyA == strategy) +  matches.Count(m => m.StrategyB == strategy);
    var scoreString = FormatScore(score, totalMaxPoints);
    const int N = 14;
    var nameString = $"{rank++,3}. {strategy.Name[..(strategy.Name.Length < N ? strategy.Name.Length : N)]}, {numberOfMatches} matches".PadRight(30);
    Console.ForegroundColor = strategy.Character == Character.Nice ? ConsoleColor.Green : ConsoleColor.Red;
    Console.WriteLine($"{nameString}: {scoreString}");
    Console.ForegroundColor = ConsoleColor.White;
}
Console.WriteLine(new string('=', 80));

static string FormatScore(int score, int maxPoints) => $"{score,6} ({(100.0 * score / maxPoints),6:N2}%)";