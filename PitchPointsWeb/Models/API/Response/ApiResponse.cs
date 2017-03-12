using Newtonsoft.Json;

namespace PitchPointsWeb.Models.API.Response
{

    /// <summary>
    /// ApiResponse is a general response for all private API requests that require some form
    /// of SignedData.
    /// </summary>
    public class ApiResponse
    {

        /// <summary>
        /// Denotes if the overall request was successful. This will be false if any step along the way has failed.
        /// For failure reasons, see ResponseCode and ResponseMessage
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Denotes the response code found in APIResponseCode
        /// </summary>
        public int ResponseCode { get; internal set; }

        /// <summary>
        /// Denotes the response message found in APIResponseCode
        /// </summary>
        public string ResponseMessage { get; internal set; }

        private ApiResponseCode MApiResponseCode { get; set; }

        /// <summary>
        /// Represents the APIResponseCode for this response. This value is ignored when this object
        /// is parsed in JSON.
        /// </summary>
        [JsonIgnore]
        public ApiResponseCode ApiResponseCode
        {
            set
            {
                MApiResponseCode = value;
                ResponseCode = (int)value;
                ResponseMessage = value.GetDescription();
                Success = value == ApiResponseCode.Success;
            }
            get
            {
                return MApiResponseCode;
            }
        }

        public ApiResponse()
        {
            ApiResponseCode = ApiResponseCode.Success;
        }

        public ApiResponse(ApiResponseCode code)
        {
            ApiResponseCode = code;
        }

    }
}