using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{
    
    public class CompetitionRule : UpdateableData
    {

        public string Description { get; set; }

    }

    public class CompetitionCategory: UpdateableData
    {

        public string Name { get; set; }

    }

    public class CompetitionType: UpdateableData
    {

        public string Type { get; set; }

    }

}