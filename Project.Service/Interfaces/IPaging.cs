namespace Project.Service.Interfaces
{
    public interface IPaging
    {
        int? Page { get; set; }
        int NumberOfObjectsPerPage { get; set; }
        int ItemsToSkip { get; set; }
        int TotalCount { get; set; }
        
    }
}
