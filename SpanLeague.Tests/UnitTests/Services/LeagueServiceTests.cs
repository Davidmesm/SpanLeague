using Microsoft.Extensions.Logging;
using Moq;
using SpanLeague.BusinessLogic.Models;
using SpanLeague.BusinessLogic.Services.Implementation;
using SpanLeague.BusinessLogic.Services;
using SpanLeague.BusinessLogic.Strategies;

namespace SpanLeague.Tests.UnitTests.Services
{
    public class LeagueServiceTests
    {
        private readonly Mock<IPointStrategy> _mockPointStrategy;
        private readonly Mock<IRankingStrategy> _mockRankingStrategy;
        private readonly Mock<ILogger<ILeagueService>> _mockLogger;
        private readonly LeagueService _leagueService;

        public LeagueServiceTests()
        {
            _mockPointStrategy = new Mock<IPointStrategy>();
            _mockRankingStrategy = new Mock<IRankingStrategy>();
            _mockLogger = new Mock<ILogger<ILeagueService>>();
            _leagueService = new LeagueService(_mockPointStrategy.Object, _mockRankingStrategy.Object, _mockLogger.Object);
        }

        [Fact]
        public void AddGameResult_ShouldUpdatePoints_WhenCalledWithValidGameResult()
        {
            var gameResult = new GameResult
            {
                TeamA = "Team A",
                TeamAScore = 3,
                TeamB = "Team B",
                TeamBScore = 2
            };

            _leagueService.AddGameResult(gameResult);

            _mockPointStrategy.Verify(strategy => strategy.UpdatePoints(gameResult, It.IsAny<Dictionary<string, Team>>()), Times.Once);
        }

        [Fact]
        public void GetGameResults_ShouldReturnAllAddedGameResults()
        {
            var gameResult1 = new GameResult { TeamA = "Team A", TeamAScore = 3, TeamB = "Team B", TeamBScore = 2 };
            var gameResult2 = new GameResult { TeamA = "Team C", TeamAScore = 1, TeamB = "Team D", TeamBScore = 0 };

            _leagueService.AddGameResult(gameResult1);
            _leagueService.AddGameResult(gameResult2);

            var results = _leagueService.GetGameResults();

            Assert.Equal(2, results.Count);
            Assert.Contains(gameResult1, results);
            Assert.Contains(gameResult2, results);
        }

        [Fact]
        public void GetRankTeams_ShouldReturnRankedTeams_WhenCalled()
        {
            // Arrange
            var teamList = new List<Team>
            {
                new Team { Name = "Team A", Points = 3 },
                new Team { Name = "Team B", Points = 2 }
            };

            _mockRankingStrategy.Setup(strategy => strategy.RankTeams(It.IsAny<List<Team>>())).Returns(teamList);

            var rankedTeams = _leagueService.GetRankTeams();

            Assert.Equal(teamList, rankedTeams);

            _mockRankingStrategy.Verify(strategy => strategy.RankTeams(It.IsAny<List<Team>>()), Times.Once);
        }
    }
}
