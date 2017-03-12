
using Newtonsoft.Json;

namespace PitchPointsWeb.Models.API.Response
{

    /// <summary>
    /// PrivateAPIResponse is a general response for all private API requests that require some form
    /// of SignedData.
    /// </summary>
    public class PrivateAPIResponse
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

        private APIResponseCode mAPIResponseCode {get;set;}

        /// <summary>
        /// Represents the APIResponseCode for this response. This value is ignored when this object
        /// is parsed in JSON.
        /// </summary>
        [JsonIgnore]
        public APIResponseCode APIResponseCode
        {
            set
            {
                mAPIResponseCode = value;
                ResponseCode = (int)value;
                ResponseMessage = value.GetDescription();
                Success = value == APIResponseCode.SUCCESS;
            }
            get
            {
                return mAPIResponseCode;
            }
        }

        public PrivateAPIResponse()
        {
            APIResponseCode = APIResponseCode.SUCCESS;
        }

        public PrivateAPIResponse(APIResponseCode code)
        {
            APIResponseCode = code;
        }

    }
}