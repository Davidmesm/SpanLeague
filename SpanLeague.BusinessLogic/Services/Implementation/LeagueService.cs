using SpanLeague.BusinessLogic.Strategies;
using SpanLeague.BusinessLogic.Models;
using Microsoft.Extensions.Logging;

namespace SpanLeague.BusinessLogic.Services.Implementation
{
    public class LeagueService : ILeagueService
    {
        private readonly Dictionary<string, Team> _teams;
        private readonly List<GameResult> _gameResults;

        private readonly IRankingStrategy _rankingStrategy;
        private readonly IPointStrategy _pointStrategy;
        private readonly ILogger<ILeagueService> _logger;

        public LeagueService(IPointStrategy pointStrategy, IRankingStrategy rankingStrategy, ILogger<ILeagueService> logger)
        {
            _teams = new Dictionary<string, Team>();
            _gameResults = new List<GameResult>();
            _pointStrategy = pointStrategy;
            _rankingStrategy = rankingStrategy;
            _logger = logger;
        }

        public void AddGameResult(GameResult gameResult)
        {
            _logger.LogInformation("Add game: {teamA} - {scoreA}, {teamB} - {score B}", 
                gameResult.TeamA, gameResult.TeamAScore, gameResult.TeamB, 
                gameResult.TeamBScore);
            _pointStrategy.UpdatePoints(gameResult, _teams);
            _gameResults.Add(gameResult);
        }
        public List<GameResult> GetGameResults()
        {
            return _gameResults;
        }

        public List<Team> GetRankTeams()
        {
            return _rankingStrategy.RankTeams(_teams.Values.ToList());
        }
    }
}