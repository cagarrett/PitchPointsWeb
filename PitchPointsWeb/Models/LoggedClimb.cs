using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{
    public class LoggedClimbResponse : PrivateAPIResponse
    {

        public LoggedClimbResponse()
        {

        }
    }

    public class LoggedClimbModel
    {
        //define climber id, witness id, route id and pass to method in route controller
        public  int climberId { get; set; }
        public int witnessId { get; set; }
        public int routeId { get; set; }
        public int falls { get; set; }
    }
}