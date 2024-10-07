using System;
using System.Collections.Generic;
using SpanLeague.BusinessLogic.Models;

namespace SpanLeague.BusinessLogic.Strategies
{
    public interface IRankingStrategy
    {
        List<Team> RankTeams(List<Team> teams);
    }
}