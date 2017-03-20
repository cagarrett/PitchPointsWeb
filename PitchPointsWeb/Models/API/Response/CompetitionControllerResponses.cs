using System.Collections.Generic;

namespace PitchPointsWeb.Models.API.Response
{
    public class CompetitionsResponse : TokenApiResponse
    {

        public List<Competition> Competitions { get; set; }

        public CompetitionsResponse()
        {
            Competitions = new List<Competition>();
        }

    }

    public class RegistrationStatusResponse : TokenApiResponse
    {

        public int CompetitionId { get; set; }
        
        public bool IsRegistered { get; set; }

        public int ClimberId { get; set; }

    }

}