using System;
using System.ComponentModel;

namespace PitchPointsWeb.Models.API.Response
{
    /// <summary>
    /// APIResponseCode holds all error codes found in the API namespace.
    /// Range 0-99: Basic error codes
    /// Range 100-199: Account error codes
    /// Range 200-299: Route error codes
    /// </summary>
    public enum ApiResponseCode
    {

        [Description("success")]
        Success = 0,

        [Description("Error authorizing user")]
        AuthError = 1,

        [Description("Internal server error")]
        InternalError = 2,

        [Description("User already registered with this email address")]
        UserAlreadyExistsEmail = 100,

        [Description("The email address does not exist")]
        UserDoesNotExistEmail = 101,

        [Description("Incorrect password")]
        IncorrectPassword = 102,

        [Description("This route has already been logged")]
        AlreadyLoggedRoute = 200,

        [Description("At least one route ID must be supplied")]
        NoRoutesSupplied = 201

    }

    public static class ApiResponseCodeExtension
    {

        /// <summary>
        /// Returns the description of this APIResponseCode
        /// </summary>
        /// <param name="value">The code to get the description from</param>
        /// <returns>A string, or null, of the APIResponseCode</returns>
        public static string GetDescription(this ApiResponseCode value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
            return attribute == null ? value.ToString() : attribute.Description;
        }

        public static T ToResponse<T>(this ApiResponseCode code) where T: ApiResponse, new()
        {
            return new T {ApiResponseCode = code};
        }

    }

}