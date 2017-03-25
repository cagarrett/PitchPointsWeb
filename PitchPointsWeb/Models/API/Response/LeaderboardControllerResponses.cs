using System.Collections.Generic;
using System.Linq;

namespace PitchPointsWeb.Models.API.Response
{
    public class LeaderboardResponse : TokenApiResponse
    {

        public Leaderboard Leaderboard { get; set; }

    }

    public class ScorecardResponse : TokenApiResponse
    {
        
        public List<ScorecardEntry> Entries { get; set; }

        public int TotalPoints
        {
            get
            {
                return Entries.Sum(e => e.Points);
            }
        }
        
    }

    public class ScorecardEntry
    {

        public int RouteID { get; set; }

        public string WitnessName { get; set; }

        public int WitnessID { get; set; }

        public int Falls { get; set; }

        public int Points { get; set; }

    }

}