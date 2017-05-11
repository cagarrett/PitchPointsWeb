namespace PitchPointsWeb.Models
{

    public class Location : UpdateableData
    {

        public string Nickname { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string ZIP { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string GooglePlaceId { get; set; }

    }

}