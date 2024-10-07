using SpanLeague.BusinessLogic.Models;

namespace SpanLeague.BusinessLogic.Strategies.Implementation
{
    public class DefaultRankingStrategy : IRankingStrategy
    {
        public List<Team> RankTeams(List<Team> teams)
        {
            return teams.OrderByDescending(t => t.Points).ThenBy(t => t.Name).ToList();
        }
    }
}