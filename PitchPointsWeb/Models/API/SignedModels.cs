using PitchPointsWeb.API;

namespace PitchPointsWeb.Models.API
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

        public bool IsValid()
        {
            return AccountVerifier.Verify(this);
        }

    }

    public class UserIDSignedData : SignedData
    {

        public int UserID { get; set; }

    }

}