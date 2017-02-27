using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{
    public class LeaderboardEntry
    {

        public int CategoryId { get; set; }

        public int UserId { get; set; }

        public bool Gender { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Points { get; set; }

        public int Falls { get; set; }
        
    }

    public class Leaderboard
    {

        public int CompetitionId { get; set; }

        public List<LeaderboardEntry> Entries { get; set; }

    }

    public class LeaderboardRequest
    {

        public int CompetitionId { get; set; }

        public int CategoryId { get; set; }

    }

}