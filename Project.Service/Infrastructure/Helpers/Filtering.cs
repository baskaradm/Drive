using Project.Service.Interfaces;

namespace Project.Service.Infrastructure.Helpers
{
    public class Filtering : IFiltering
    {
        public string SearchString { get; set; }
        public string CurrentFilter { get; set; }
        public string FilterBy { get; set; }

        public Filtering(string searchString, string currentFilter)
        {
            CurrentFilter = currentFilter;
            SearchString = searchString;
        }

        public bool ShouldApplyFilters()
        {
            if(!string.IsNullOrEmpty(CurrentFilter))
            {
                FilterBy = CurrentFilter;
                return true;
            }

            if (!string.IsNullOrEmpty(SearchString))
            {
                FilterBy = SearchString;
                return true;
            }

            return false;
        }
    }
}