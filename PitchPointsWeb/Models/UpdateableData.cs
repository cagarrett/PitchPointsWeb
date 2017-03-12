namespace PitchPointsWeb.Models
{
    public class UpdateableData
    {

        private int? _mId;

        /// <summary>
        /// Represents the ID of this database object in the database. Once this value is set, it cannot be updated. This is to prevent any issues with updating this entry in the database.
        /// </summary>
        public int? Id
        {
            get { return _mId; }
            set { _mId = _mId ?? value; }
        }

    }
}