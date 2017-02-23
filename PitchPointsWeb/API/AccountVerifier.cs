using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Security;
using PitchPointsWeb.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace PitchPointsWeb.API
{
    public class AccountVerifier
    {

        private static readonly string KEY_PAIR_ALGORITHM = "ECDSA";

        private static readonly string SIGNER_METHOD = "ECDSAWITHSHA256";

        private static readonly string CURVE_NAME = "secp256r1";

        public static PublicKeyUserModel GetPublicKeyFor(int pubKeyId)
        {
            PublicKeyUserModel model = new PublicKeyUserModel();
            model.Invalid = true;
            var connection = APICommon.GetConnection();
            connection.Open();
            using (var command = new SqlCommand("GetPublicKeyForUser", connection))
            {
                command.CommandType = System.Data.CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@pubKeyId", pubKeyId);
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    model.PublicKey = APICommon.readObject(reader, "PublicKey", new byte[] { });
                    model.ExpiryDate = APICommon.readObject(reader, "ExpiryDate", DateTime.Now);
                    model.Invalid = APICommon.readObject(reader, "Invalid", (byte)0) == 1;
                }
                reader.Close();
            }
            connection.Close();
            return model;
        }

        internal static bool InternalVerify(SignedData data)
        {
            return Verify(data, GetPublicKeyFor(data.PublicKeyID));
        }

        /// <summary>
        /// Verifies that the SignedData object is valid with the provided publicKey
        /// </summary>
        /// <param name="data">SignedData received from an API call</param>
        /// <param name="publicKey">The public key used to verify data</param>
        /// <returns>true if the Signature and Data within SignedData matches the publicKey digest</returns>
        public static bool Verify(SignedData data, PublicKeyUserModel publicKey)
        {
            if (publicKey == null || publicKey.Invalid) return false;
            var signer = SignerUtilities.GetSigner(SIGNER_METHOD);
            var inputData = new ASCIIEncoding().GetBytes(data.Data);
            var domain = GetECDomain();
            var point = domain.Curve.DecodePoint(publicKey.PublicKey);
            signer.Init(false, new ECPublicKeyParameters(point, domain));
            signer.BlockUpdate(inputData, 0, inputData.Length);
            return signer.VerifySignature(StringToByteArray(data.Signature));
        }

        /// <summary>
        /// Signs a string with a provided private key
        /// </summary>
        /// <param name="data">The data to sign</param>
        /// <param name="privateKey">The base64 private key</param>
        public static string Sign(string data, string privateKey)
        {
            var signer = SignerUtilities.GetSigner(SIGNER_METHOD);
            var privKey = new BigInteger(Convert.FromBase64String(privateKey));
            var inputData = new ASCIIEncoding().GetBytes(data);
            signer.Init(true, new ECPrivateKeyParameters(privKey, GetECDomain()));
            signer.BlockUpdate(inputData, 0, inputData.Length);
            return BytesToHexString(signer.GenerateSignature());
        }

        /// <summary>
        /// Generates an ECDSA key pair from secp256r1 curve
        /// </summary>
        /// <returns>A tuple of bytes. Item1 is the public key, Item2 is the private key.</returns>
        public static Tuple<byte[], byte[]> GenerateKeyPair()
        {
            var generator = new ECKeyPairGenerator(KEY_PAIR_ALGORITHM);
            var keyGenParameter = new ECKeyGenerationParameters(GetECDomain(), new SecureRandom());
            generator.Init(keyGenParameter);
            var keyPair = generator.GenerateKeyPair();
            var publicKey = (ECPublicKeyParameters)keyPair.Public;
            byte[] pubKey = publicKey.Q.GetEncoded();
            var privateKey = (ECPrivateKeyParameters)keyPair.Private;
            byte[] privKey = privateKey.D.ToByteArray();
            return new Tuple<byte[], byte[]>(pubKey, privKey);
        }

        private static ECDomainParameters GetECDomain()
        {
            var ecp = SecNamedCurves.GetByName(CURVE_NAME);
            return new ECDomainParameters(ecp.Curve, ecp.G, ecp.N, ecp.H, ecp.GetSeed());
        }

        /// <summary>
        /// Converts a hexadecimal string to a byte array
        /// </summary>
        /// <param name="hex">A string in hexadecimal form</param>
        /// <returns>A byte[] representation of hex</returns>
        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length).Where(x => x % 2 == 0).Select(x => Convert.ToByte(hex.Substring(x, 2), 16)).ToArray();
        }

        /// <summary>
        /// Converts a byte[] to a hexadecimal string.
        /// This was taken from http://stackoverflow.com/a/623114
        /// </summary>
        /// <param name="bytes">The bytes to convert</param>
        /// <returns>A hexadecimal string</returns>
        private static string BytesToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }

    }
}