using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{

    public class SignedData
    {

        /// <summary>
        /// The ID of the public key in UserPublicKey
        /// </summary>
        public int PublicKeyID { get; set; }

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

    public class PublicKeyUserModel
    {

        public byte[] PublicKey { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Invalid { get; set; }

        public PublicKeyUserModel()
        {

        }

    }

}