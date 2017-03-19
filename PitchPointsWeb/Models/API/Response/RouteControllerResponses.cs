using System.Collections.Generic;

namespace PitchPointsWeb.Models.API.Response
{

    public class RoutesResponse : ApiResponse
    {
        
        public List<PublicRoute> Routes { get; set; }

        public RoutesResponse()
        {
            Routes = new List<PublicRoute>();
        }

    }

    public class CompetitionRoutesResponse : RoutesResponse
    {

        public int CompetitionId { get; set; }

    }


}