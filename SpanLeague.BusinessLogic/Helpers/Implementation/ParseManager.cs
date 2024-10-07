using Microsoft.Extensions.Logging;
using SpanLeague.BusinessLogic.Models;

namespace SpanLeague.BusinessLogic.Helpers.Implementation
{
    public class ParseManager : IParseManager
    {
        private readonly ILogger<IParseManager> _logger;

        public ParseManager(ILogger<IParseManager> logger)
        {
            _logger = logger;
        }

        public GameResult? ParseFromString(string line)
        {
            try
            {
                var parts = line.Split(',');

                var teamAInfo = parts[0].Trim().Split(' ');
                var teamBInfo = parts[1].Trim().Split(' ');

                //Use take for names with spaces
                var teamAName = string.Join(' ', teamAInfo.Take(teamAInfo.Length - 1));
                var teamAScore = int.Parse(teamAInfo.Last());

                var teamBName = string.Join(' ', teamBInfo.Take(teamBInfo.Length - 1));
                var teamBScore = int.Parse(teamBInfo.Last());

                return new GameResult()
                {
                    TeamA = teamAName,
                    TeamAScore = teamAScore,
                    TeamB = teamBName,
                    TeamBScore = teamBScore
                };
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error parsing line {line}", line);
                return null;
            }
        }

        public string[] ParseToStringArray(List<Team> rankedTeams)
        {
            List<string> rankedTeamsResult = new List<string>();
            int currentRank = 1;
            int previousPoints = -1;
            int position = 1;

            foreach (var team in rankedTeams)
            {
                if (team.Points != previousPoints)
                {
                    currentRank = position;
                }

                rankedTeamsResult.Add($"{currentRank}. {team.Name}, {team.Points} pt{(team.Points == 1 ? "" : "s")}");

                previousPoints = team.Points;
                position++;
            }

            return rankedTeamsResult.ToArray();
        }
    }
}