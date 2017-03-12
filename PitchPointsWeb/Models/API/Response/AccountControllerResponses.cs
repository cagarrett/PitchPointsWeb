using System.Collections.Generic;

namespace PitchPointsWeb.Models.API.Response
{

    public class InsertUserResponse : PrivateAPIResponse
    {

        public int UserId;

        public PrivateKeyInfo PrivateKeyInfo;

        public InsertUserResponse() { }

    }

    public class RegisterAccountResponse : PrivateAPIResponse
    {

        public PrivateKeyInfo PrivateKey { get; set; }

        public RegisterAccountResponse() { }

    }

    public class LoginAccountResponse : PrivateAPIResponse
    {

        public PrivateKeyInfo PrivateKey { get; set; }

        public LoginAccountResponse() { }

    }

    public class UserSnapshot : PrivateAPIResponse
    {

        public int Points { get; set; }

        public int Falls { get; set; }

        public int ParticipatedCompetitions { get; set; }

        public List<Competition> UpcomingCompetitions { get; set; }

        public UserSnapshot() { }

    }

}