namespace PitchPointsWeb.Models.API
{

    public class LoggedClimbModel
    {

        public int ClimberId { get; set; }

        public int WitnessId { get; set; }

        public int RouteId { get; set; }

        public int Falls { get; set; }

    }

    public class CompetitionRoutesModel
    {
        
        public int CompetitionId { get; set; }

    }

}