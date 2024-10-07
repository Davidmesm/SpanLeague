using System;
using System.Collections.Generic;
using SpanLeague.BusinessLogic.Models;

namespace SpanLeague.BusinessLogic.Strategies
{
    public interface IPointStrategy
    {
        void UpdatePoints(GameResult result, Dictionary<string, Team> teams);
    }
}