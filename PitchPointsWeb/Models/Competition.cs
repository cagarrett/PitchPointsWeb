using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PitchPointsWeb.Models
{
    public class Competition : UpdateableData
    {

        public string CompetitionTitle { get; set; }

        public Location Location { get; set; }

        private List<CompetitionRule> mRules = new List<CompetitionRule>();

        public IEnumerable<CompetitionRule> Rules
        {
            get { return mRules.AsReadOnly(); }
        }

        private List<CompetitionType> mTypes = new List<CompetitionType>();

        public IEnumerable<CompetitionType> Types
        {
            get { return mTypes.AsReadOnly(); }
        }

        private List<CompetitionCategory> mCategories = new List<CompetitionCategory>();

        public IEnumerable<CompetitionCategory> Categories
        {
            get { return mCategories.AsReadOnly(); }
        }

        public string Details { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public void AddRule(CompetitionRule rule)
        {
            if (mRules.Count(temp => temp.ID == rule.ID) == 0)
            {
                mRules.Add(rule);
            }
        }

        public void RemoveRule(CompetitionRule rule)
        {
            mRules.RemoveAll(temp => temp.ID == rule.ID);
        }

        public void AddType(CompetitionType type)
        {
            if (mTypes.Count(temp => temp.ID == type.ID) == 0)
            {
                mTypes.Add(type);
            }
        }

        public void RemoveType(CompetitionType type)
        {
            mTypes.RemoveAll(temp => temp.ID == type.ID);
        }

        public void AddCategory(CompetitionCategory category)
        {
            if (mCategories.Count(temp => temp.ID == category.ID) == 0)
            {
                mCategories.Add(category);
            }
        }

        public void RemoveCategory(CompetitionCategory category)
        {
            mCategories.RemoveAll(temp => temp.ID == category.ID);
        }

    }
}