namespace Project.Service.Interfaces
{
    public interface ISorting
    {
        string SortBy { get; set; }
        string SortByName { get; set; }
        string SortByAbbreviation { get; set; }
        string SortById { get; set; }
    }
}
