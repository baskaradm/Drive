namespace Project.Service.Interfaces
{
    public interface IFiltering
    {
        string SearchString { get; set; }
        string CurrentFilter { get; set; }
        string FilterBy { get; set; }

        bool ShouldApplyFilters();
    }
}
