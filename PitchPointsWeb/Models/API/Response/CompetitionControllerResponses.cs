using System.Collections.Generic;

namespace PitchPointsWeb.Models.API.Response
{
    public class CompetitionsResponse : ApiResponse
    {

        public List<Competition> Competitions { get; set; }

        public CompetitionsResponse()
        {
            Competitions = new List<Competition>();
        }

    }
}