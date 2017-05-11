using System;

namespace PitchPointsWeb.Models.API
{

    /// <summary>
    /// Represents a user that is used in the API during registration
    /// </summary>
    public class RegisterModel
    {

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Password { get; set; }

    }

    /// <summary>
    /// Represents a user that is used in the API during logging in
    /// </summary>
    public class LoginModel
    {

        public string Email { get; set; }

        public string Password { get; set; }

    }

}