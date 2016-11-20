using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{
    public class PrivateAPIResponse
    {

        public bool Success { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public PrivateAPIResponse()
        {

        }

    }
}