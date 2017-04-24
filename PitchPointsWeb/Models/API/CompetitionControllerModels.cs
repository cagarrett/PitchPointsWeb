
namespace PitchPointsWeb.Models.API
{
    public class CompetitionRegistrationModel: TokenModel
    {

        /// <summary>
        /// Represents the ID for the competition that the user wants to interact with
        /// </summary>
        public int CompetitionId { get; set; }

        /// <summary>
        /// True if the user wants to register for CompetitionId, false if they want to unregister
        /// </summary>
        public int Register { get; set; }

    }
}