using Microsoft.Extensions.Logging;
using Moq;
using SpanLeague.BusinessLogic.Models;
using SpanLeague.BusinessLogic.Services.Implementation;
using SpanLeague.BusinessLogic.Services;
using SpanLeague.BusinessLogic.Strategies.Implementation;
using SpanLeague.BusinessLogic.Helpers.Implementation;
using SpanLeague.BusinessLogic.Helpers;

namespace SpanLeague.Tests.IntegrationTests
{
    public class DefaultLeagueTests
    {
        [Fact]
        public void AddGameResult_ShouldUpdateTeamsAndRankCorrectly()
        {
            var pointStrategy = new DefaultPointStrategy();
            var rankingStrategy = new DefaultRankingStrategy();
            var parseManager = new ParseManager(new Mock<ILogger<IParseManager>>().Object);
            var fileManager = new FileManager(new Mock<ILogger<IFileManager>>().Object);
            var leagueService = new LeagueService(pointStrategy, rankingStrategy, new Mock<ILogger<ILeagueService>>().Object);

            var filePath = "testInput.txt";
            var expectedContent = new[] { 
                "Lions 3, Snakes 3", 
                "Tarantulas 1, FC Awesome 0",
                "Lions 1, FC Awesome 1",
                "Tarantulas 3, Snakes 1",
                "Lions 4, Grouches 0" 
            };

            File.WriteAllLines(filePath, expectedContent);

            var inputData = fileManager.ReadTextFile(filePath);

            var gameResults = inputData
                            .Select(line => parseManager.ParseFromString(line))
                            .Where(result => result != null)
                            .ToList();

            gameResults.ForEach(gameResult =>
            {
                leagueService.AddGameResult(gameResult);
            });

            var rankedTeams = leagueService.GetRankTeams();
            var rankedTeamsToPrint = parseManager.ParseToStringArray(rankedTeams);

            List<Team> expectedRankTeams = new List<Team>()
            {
                new Team() { Name="Tarantulas", Points=6},
                new Team() { Name="Lions", Points=5},
                new Team() { Name="FC Awesome", Points=1},
                new Team() { Name="Snakes", Points=1},
                new Team() { Name="Grouches", Points=0},
            };

            for(int i = 0; i < rankedTeams.Count; i++)
            {
                Assert.Equal(expectedRankTeams[i].Name, rankedTeams[i].Name);
                Assert.Equal(expectedRankTeams[i].Points, rankedTeams[i].Points);
            }

            Assert.Equal("1. Tarantulas, 6 pts", rankedTeamsToPrint[0]);
            Assert.Equal("2. Lions, 5 pts", rankedTeamsToPrint[1]);
            Assert.Equal("3. FC Awesome, 1 pt", rankedTeamsToPrint[2]);
            Assert.Equal("3. Snakes, 1 pt", rankedTeamsToPrint[3]);
            Assert.Equal("5. Grouches, 0 pts", rankedTeamsToPrint[4]);
        }
    }
}
