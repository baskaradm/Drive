using Project.Service.Interfaces;
using System;

namespace Project.Service.Infrastructure.Helpers
{
    public class Sorting : ISorting
    {
        public string SortBy { get; set; }
        public string SortByName { get; set; }
        public string SortByAbbreviation { get; set; }
        public string SortById { get; set; }

        public Sorting(string sortBy)
        {
            SortBy = sortBy;
            SortByName = String.IsNullOrEmpty(sortBy) ? "name_desc" : "";
            SortByAbbreviation = sortBy == "Abbreviation" ? "abbreviation_desc" : "Abbreviation";
            SortById = sortBy == "VehicleMakeId" ? "vehiclemakeid_desc" : "VehicleMakeId";
        }
    }

}