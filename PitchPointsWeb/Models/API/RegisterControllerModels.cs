namespace PitchPointsWeb.Models.API
{

    public class RegisteredClimberModel: TokenModel
    {

        public int ClimberId { get; set; }

        public int Category { get; set; }

        public int CompetitionId { get; set; }

        //public int Register { get; set; }


    }

    /*public class CompetitionRoutesModel
    {
        
        public int CompetitionId { get; set; }

    }*/

}