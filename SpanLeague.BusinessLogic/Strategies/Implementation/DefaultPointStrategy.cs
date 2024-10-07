using SpanLeague.BusinessLogic.Models;

namespace SpanLeague.BusinessLogic.Strategies.Implementation
{
    public class DefaultPointStrategy : IPointStrategy
    {
        public void UpdatePoints(GameResult result, Dictionary<string, Team> teams)
        {
            //If team doesn't exists, create new Team
            if (!teams.ContainsKey(result.TeamA))
            {
                teams[result.TeamA] = new Team(result.TeamA);
            }
            if (!teams.ContainsKey(result.TeamB))
            {
                teams[result.TeamB] = new Team(result.TeamB);
            }


            if (result.TeamAScore > result.TeamBScore)
            {
                teams[result.TeamA].Points += 3;
            }
            else if (result.TeamAScore < result.TeamBScore)
            {
                teams[result.TeamB].Points += 3;
            }
            else
            {
                teams[result.TeamA].Points += 1;
                teams[result.TeamB].Points += 1;
            }
        }
    }
}