using SpanLeague.BusinessLogic.Models;
using SpanLeague.BusinessLogic.Strategies.Implementation;

namespace SpanLeague.Tests.UnitTests.Strategies
{
    public class DefaultPointStrategyTests
    {
        private readonly DefaultPointStrategy _pointStrategy;

        public DefaultPointStrategyTests()
        {
            _pointStrategy = new DefaultPointStrategy();
        }

        [Fact]
        public void UpdatePoints_ShouldAddThreePointsToTeamA_WhenTeamAWins()
        {
            var teams = new Dictionary<string, Team>();
            var gameResult = new GameResult
            {
                TeamA = "Team A",
                TeamAScore = 3,
                TeamB = "Team B",
                TeamBScore = 2
            };

            _pointStrategy.UpdatePoints(gameResult, teams);

            Assert.True(teams.ContainsKey("Team A"));
            Assert.True(teams.ContainsKey("Team B"));
            Assert.Equal(3, teams["Team A"].Points);
            Assert.Equal(0, teams["Team B"].Points);
        }

        [Fact]
        public void UpdatePoints_ShouldAddThreePointsToTeamB_WhenTeamBWins()
        {
            var teams = new Dictionary<string, Team>();
            var gameResult = new GameResult
            {
                TeamA = "Team A",
                TeamAScore = 1,
                TeamB = "Team B",
                TeamBScore = 4
            };

            _pointStrategy.UpdatePoints(gameResult, teams);

            Assert.True(teams.ContainsKey("Team A"));
            Assert.True(teams.ContainsKey("Team B"));
            Assert.Equal(0, teams["Team A"].Points);
            Assert.Equal(3, teams["Team B"].Points);
        }

        [Fact]
        public void UpdatePoints_ShouldAddOnePointToEachTeam_WhenDraw()
        {
            var teams = new Dictionary<string, Team>();
            var gameResult = new GameResult
            {
                TeamA = "Team A",
                TeamAScore = 2,
                TeamB = "Team B",
                TeamBScore = 2
            };

            _pointStrategy.UpdatePoints(gameResult, teams);

            Assert.True(teams.ContainsKey("Team A"));
            Assert.True(teams.ContainsKey("Team B"));
            Assert.Equal(1, teams["Team A"].Points);
            Assert.Equal(1, teams["Team B"].Points);
        }

    }
}
