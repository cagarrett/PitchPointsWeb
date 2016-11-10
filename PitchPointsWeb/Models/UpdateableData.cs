using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{
    public class UpdateableData
    {

        private int? mID;

        /// <summary>
        /// Represents the ID of this database object in the database. Once this value is set, it cannot be updated. This is to prevent any issues with updating this entry in the database.
        /// </summary>
        public int? ID
        {
            get { return mID; }
            set { mID = mID ?? value; }
        }

    }
}