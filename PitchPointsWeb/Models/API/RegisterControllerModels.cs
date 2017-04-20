namespace PitchPointsWeb.Models.API
{

    public class RegisteredClimberModel: TokenModel
    {

        //public int ClimberId { get; set; }

        public string Email { get; internal set; }

        public int Category { get; set; }

        public int CompetitionId { get; set; }

        //public byte Register { get; set; }


    }

    /*public class CompetitionRoutesModel
    {
        
        public int CompetitionId { get; set; }

    }*/

}