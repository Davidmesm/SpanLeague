using System;
using System.Collections.Generic;
using SpanLeague.BusinessLogic.Models;

namespace SpanLeague.BusinessLogic.Services
{
    public interface ILeagueService
    {
        void AddGameResult(GameResult gameResult);
        List<GameResult> GetGameResults();
        List<Team> GetRankTeams();
    }
}