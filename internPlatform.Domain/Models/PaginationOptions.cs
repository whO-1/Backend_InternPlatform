
namespace internPlatform.Domain.Models
{
    public class PaginationOptions
    {
        public int PageSize { get; set; }
        public int CurrentPage  { get; set; }

        public PaginationOptions(int pageSize = 10, int currentPage = 1) { 
            PageSize = pageSize;
            CurrentPage = currentPage;  
        }
    }
}
