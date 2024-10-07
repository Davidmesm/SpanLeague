using SpanLeague.BusinessLogic.Services;
using SpanLeague.BusinessLogic.Services.Implementation;
using SpanLeague.BusinessLogic.Helpers;
using SpanLeague.BusinessLogic.Helpers.Implementation;
using SpanLeague.BusinessLogic.Strategies;
using SpanLeague.BusinessLogic.Strategies.Implementation;
using Microsoft.Extensions.DependencyInjection;
using SpanLeague.BusinessLogic.Models;
using Microsoft.Extensions.Logging;


if (args.Length == 0)
{
    Console.WriteLine("Input file missing.");
}

string filePath = args[0];

if (!File.Exists(filePath))
{
    Console.WriteLine($"Error: File '{filePath}' does not exist.");
    return;
}

// Set up Dependency Injection
var serviceProvider = new ServiceCollection()
                .AddSingleton<ILeagueService, LeagueService>()
                .AddSingleton<IParseManager, ParseManager>()
                .AddSingleton<IFileManager, FileManager>()
                .AddSingleton<IPointStrategy, DefaultPointStrategy>()
                .AddSingleton<IRankingStrategy, DefaultRankingStrategy>()
                .AddLogging(configure => configure.AddConsole())
                .BuildServiceProvider();

var leagueService = serviceProvider.GetRequiredService<ILeagueService>();
var fileManager = serviceProvider.GetRequiredService<IFileManager>();
var parseManager = serviceProvider.GetRequiredService<IParseManager>();

string[]? input = null;

try
{
    input = fileManager.ReadTextFile(filePath);
}
catch (IOException ioEx)
{
    Console.WriteLine($"Error reading the file: {ioEx.Message}");
    return;
}
catch (Exception ex)
{
    Console.WriteLine($"An unexpected error occurred while reading the file: {ex.Message}");
    return;
}

List<GameResult> gameResults;

try
{
    gameResults = input
        .Select(line => parseManager.ParseFromString(line))
        .Where(result => result != null) // Filter out any null results due to parsing errors
        .ToList();
}
catch (Exception ex)
{
    Console.WriteLine($"Error parsing game results: {ex.Message}");
    return;
}

try
{
    gameResults.ForEach(gameResult =>
    {
        leagueService.AddGameResult(gameResult);
    });
}
catch (Exception ex)
{
    Console.WriteLine($"Error adding game results to league service: {ex.Message}");
    return;
}


try
{
    var rankedTeams = leagueService.GetRankTeams();

    var rankedTeamsToPrint = parseManager.ParseToStringArray(rankedTeams);

    rankedTeamsToPrint.ToList().ForEach(teamRank => Console.WriteLine(teamRank));
} 
catch (Exception ex)
{
    Console.WriteLine($"Error printing ranked teams: {ex.Message}");
}


