using SpanLeague.BusinessLogic.Models;
namespace SpanLeague.BusinessLogic.Helpers
{
    public interface IParseManager
    {
        GameResult? ParseFromString(string line);
        string[] ParseToStringArray(List<Team> rankedTeams);
    }
}