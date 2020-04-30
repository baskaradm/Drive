using Project.Service.Interfaces;

namespace Project.Service.Infrastructure.Helpers
{
    public class Paging : IPaging
    {
        public int? Page { get; set; }
        public int  NumberOfObjectsPerPage { get; set; }
        public int  ItemsToSkip{ get; set; }
        public int  TotalCount { get; set; }


        public Paging(int? page)
        {
            NumberOfObjectsPerPage = 5;
            Page = page;
            ItemsToSkip = NumberOfObjectsPerPage * ((Page??1)-1);
        }
    }
}
