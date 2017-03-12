using PitchPointsWeb.Models.API.Response;
using System.ComponentModel;

namespace PitchPointsWeb.Models.API
{
    /// <summary>
    /// APIResponseCode holds all error codes found in the API namespace.
    /// Range 0-99: Basic error codes
    /// Range 100-199: Account error codes
    /// Range 200-299: Log climb error codes
    /// </summary>
    public enum APIResponseCode
    {

        [Description("success")]
        SUCCESS = 0,

        [Description("Error authorizing user")]
        AUTH_ERROR = 1,

        [Description("Internal server error")]
        INTERNAL_ERROR = 2,

        [Description("User already registered with this email address")]
        USER_ALREADY_EXISTS_EMAIL = 100,

        [Description("The email address does not exist")]
        USER_DOES_NOT_EXIST_EMAIL = 101,

        [Description("Incorrect password")]
        INCORRECT_PASSWORD = 102,

        [Description("This route has already been logged")]
        ALREADY_LOGGED_ROUTE = 200,

    }

    public static class APIResponseCodeExtension
    {

        /// <summary>
        /// Returns the description of this APIResponseCode
        /// </summary>
        /// <param name="code">The code to get the description from</param>
        /// <returns>A string, or null, of the APIResponseCode</returns>
        public static string GetDescription<APIResponseCode>(this APIResponseCode code)
        {
            var type = code.GetType();
            var memberInfo = type.GetMember(code.ToString());
            var attributes = memberInfo.Length > 0 ? memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false) : null;
            if (attributes != null && attributes.Length > 0)
            {
                return attributes[0].ToString();
            }
            return null;
        }

        public static APIResponseCode From(int value)
        {
            return (APIResponseCode)value;
        }

        public static PrivateAPIResponse ToResponse(this APIResponseCode code)
        {
            return new PrivateAPIResponse(code);
        }

    }

}