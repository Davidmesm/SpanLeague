using SpanLeague.BusinessLogic.Models;
using SpanLeague.BusinessLogic.Strategies.Implementation;

namespace SpanLeague.Tests.UnitTests.Strategies
{
    public class DefaultRankingStrategyTests
    {
        private readonly DefaultRankingStrategy _rankingStrategy;

        public DefaultRankingStrategyTests()
        {
            _rankingStrategy = new DefaultRankingStrategy();
        }

        [Fact]
        public void RankTeams_ShouldSortTeamsByPointsDescendingAndNameAscending()
        {
            var teams = new List<Team>
            {
                new Team { Name = "Team C", Points = 5 },
                new Team { Name = "Team A", Points = 3 },
                new Team { Name = "Team B", Points = 5 },
                new Team { Name = "Team D", Points = 2 }
            };

            var expectedOrder = new List<Team>
            {
                new Team { Name = "Team B", Points = 5 },
                new Team { Name = "Team C", Points = 5 },
                new Team { Name = "Team A", Points = 3 },
                new Team { Name = "Team D", Points = 2 }
            };

            var rankedTeams = _rankingStrategy.RankTeams(teams);

            Assert.Equal(expectedOrder.Count, rankedTeams.Count);

            for (int i = 0; i < expectedOrder.Count; i++)
            {
                Assert.Equal(expectedOrder[i].Name, rankedTeams[i].Name);
                Assert.Equal(expectedOrder[i].Points, rankedTeams[i].Points);
            }
        }

        [Fact]
        public void RankTeams_ShouldHandleEmptyList()
        {
            var teams = new List<Team>();

            var rankedTeams = _rankingStrategy.RankTeams(teams);

            Assert.Empty(rankedTeams);
        }
    }
}
