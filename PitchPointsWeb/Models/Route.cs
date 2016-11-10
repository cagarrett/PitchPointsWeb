namespace PitchPointsWeb.Models
{
    public class Route : UpdateableData
    {

        public string Name { get; set; }

        public int CategoryID { get; set; }

        public int MaxPoints { get; set; }

        public int PointDeductionPerFall { get; set; }

    }

    /// <summary>
    /// PublicRoute is a Route that is configured to be returned from the public API. In order for a Route to be updated, please refer to Route
    /// </summary>
    public class PublicRoute
    {
        
        public int? ID { get; set; }

        public string Name { get; set; }

        public string CategoryName { get; set; }

        public int MaxPoints { get; set; }

        public int PointDeductionPerFall { get; set; }

    }

}