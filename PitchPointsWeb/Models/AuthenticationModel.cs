using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{

    public class SignedData
    {

        /// <summary>
        /// The plain text data that was signed
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// A hexadecimal representation received from a ECDSA-secp256r1 signature
        /// </summary>
        public string Signature { get; set; }

        public SignedData()
        {

        }

    }
}