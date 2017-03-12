using System;
using System.Collections.Generic;
using System.Linq;

namespace PitchPointsWeb.Models
{
    public class Competition : UpdateableData
    {

        public string CompetitionTitle { get; set; }

        public Location Location { get; set; }

        private readonly List<CompetitionRule> _mRules = new List<CompetitionRule>();

        public IEnumerable<CompetitionRule> Rules => _mRules.AsReadOnly();

        private readonly List<CompetitionType> _mTypes = new List<CompetitionType>();

        public IEnumerable<CompetitionType> Types => _mTypes.AsReadOnly();

        private readonly List<CompetitionCategory> _mCategories = new List<CompetitionCategory>();

        public IEnumerable<CompetitionCategory> Categories => _mCategories.AsReadOnly();

        public string Details { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public void AddRule(CompetitionRule rule)
        {
            if (_mRules.Count(temp => temp.Id == rule.Id) == 0)
            {
                _mRules.Add(rule);
            }
        }

        public void RemoveRule(CompetitionRule rule)
        {
            _mRules.RemoveAll(temp => temp.Id == rule.Id);
        }

        public void AddType(CompetitionType type)
        {
            if (_mTypes.Count(temp => temp.Id == type.Id) == 0)
            {
                _mTypes.Add(type);
            }
        }

        public void RemoveType(CompetitionType type)
        {
            _mTypes.RemoveAll(temp => temp.Id == type.Id);
        }

        public void AddCategory(CompetitionCategory category)
        {
            if (_mCategories.Count(temp => temp.Id == category.Id) == 0)
            {
                _mCategories.Add(category);
            }
        }

        public void RemoveCategory(CompetitionCategory category)
        {
            _mCategories.RemoveAll(temp => temp.Id == category.Id);
        }

    }

    public class CompetitionRule : UpdateableData
    {

        public string Description { get; set; }

    }

    public class CompetitionCategory : UpdateableData
    {

        public string Name { get; set; }

    }

    public class CompetitionType : UpdateableData
    {

        public string Type { get; set; }

    }

}