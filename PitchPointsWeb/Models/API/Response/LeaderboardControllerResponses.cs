namespace PitchPointsWeb.Models.API.Response
{
    public class LeaderboardResponse : TokenApiResponse
    {

        public Leaderboard Leaderboard { get; set; }

    }

    public class UserSnapshotsResponse : TokenApiResponse
    {

        public int Points { get; set; }
        
    }

}