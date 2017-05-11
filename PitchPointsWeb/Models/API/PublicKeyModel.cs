using System;

namespace PitchPointsWeb.Models.API
{

    public class PublicKeyUserModel
    {

        public byte[] PublicKey { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Invalid { get; set; }

    }

}