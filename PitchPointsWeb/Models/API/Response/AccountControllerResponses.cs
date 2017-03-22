using System.Collections.Generic;

namespace PitchPointsWeb.Models.API.Response
{

    public class PrivateApiResponse : TokenApiResponse
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }

    public class InsertUserResponse : PrivateApiResponse
    {

        public int UserId { get; set; }

    }

    public class UserSnapshotResponse : TokenApiResponse
    {

        public int Points { get; set; }

        public int Falls { get; set; }

        public int ParticipatedCompetitions { get; set; }

        public List<Competition> UpcomingCompetitions { get; set; }

        public UserSnapshotResponse()
        {
            UpcomingCompetitions = new List<Competition>();
        }

    }

}