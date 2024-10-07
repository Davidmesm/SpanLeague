using System;

namespace SpanLeague.BusinessLogic.Models
{
    public class Team
    {
        public string Name { get; set; }
        public int Points { get; set; }

        public Team()
        {
            Name = string.Empty;
        }

        public Team(string name)
        {
            Name = name;
            Points = 0;
        }
    }
}