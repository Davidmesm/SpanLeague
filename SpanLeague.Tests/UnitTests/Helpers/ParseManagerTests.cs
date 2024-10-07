using Microsoft.Extensions.Logging;
using Moq;
using SpanLeague.BusinessLogic.Helpers.Implementation;
using SpanLeague.BusinessLogic.Helpers;
using SpanLeague.BusinessLogic.Models;

namespace SpanLeague.Tests.UnitTests.Helpers
{
    public class ParseManagerTests
    {
        private readonly Mock<ILogger<IParseManager>> _mockLogger;
        private readonly ParseManager _parseManager;

        public ParseManagerTests()
        {
            _mockLogger = new Mock<ILogger<IParseManager>>();
            _parseManager = new ParseManager(_mockLogger.Object);
        }

        [Fact]
        public void ParseFromString_ShouldReturnGameResult_WhenInputIsValid()
        {
            var line = "Team A 3, Team B 2";
            var expected = new GameResult
            {
                TeamA = "Team A",
                TeamAScore = 3,
                TeamB = "Team B",
                TeamBScore = 2
            };

            var result = _parseManager.ParseFromString(line);

            Assert.NotNull(result);
            Assert.Equal(expected.TeamA, result.TeamA);
            Assert.Equal(expected.TeamAScore, result.TeamAScore);
            Assert.Equal(expected.TeamB, result.TeamB);
            Assert.Equal(expected.TeamBScore, result.TeamBScore);
        }

        [Fact]
        public void ParseFromString_ShouldReturnNull_WhenInputIsInvalid()
        {
            var line = "Invalid input line";

            var result = _parseManager.ParseFromString(line);

            Assert.Null(result);
        }

        [Fact]
        public void ParseToStringArray_ShouldReturnRankedTeams_WhenInputIsValid()
        {
            var rankedTeams = new List<Team>
            {
                new Team() { Name = "Team A", Points = 3 },
                new Team() { Name = "Team B", Points = 2 },
                new Team() { Name = "Team C", Points = 2 },
                new Team() { Name = "Team D", Points = 1 }
            };

            var expected = new[]
            {
                "1. Team A, 3 pts",
                "2. Team B, 2 pts",
                "2. Team C, 2 pts",
                "4. Team D, 1 pt"
            };

            var result = _parseManager.ParseToStringArray(rankedTeams);

            Assert.Equal(expected, result);
        }
    }
}
