using System.Collections.Generic;

1namespace PitchPointsWeb.Models
{

    public class RegisterAccountResponse : PrivateAPIResponse
    {

        public PrivateKeyInfo PrivateKey { get; set; }

        public RegisterAccountResponse()
        {

        }

    }

    public class LoginAccountResponse : PrivateAPIResponse
    {

        public PrivateKeyInfo PrivateKey { get; set; }

        public LoginAccountResponse()
        {

        }

    }

    public class UserSnapshot
    {

        public int Points { get; set; }

        public int Falls { get; set; }

        public int ParticipatedCompetitions { get; set; }

        public List<Competition> UpcomingCompetitions { get; set; }

    }

    public class PrivateKeyInfo
    {

        public string PrivateKey { get; set; }

        public int PublicKeyId { get; set; }

        public string Username { get; set; }

        public PrivateKeyInfo()
        {

        }

    }

}