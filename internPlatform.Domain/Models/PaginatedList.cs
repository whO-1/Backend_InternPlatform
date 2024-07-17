using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace internPlatform.Domain.Models
{
    public class PaginatedList<T> : List<T>
    {
        public int CurrentPage {  get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public PaginatedList( List<T> list, int count, int currentPage, int pageSize ) : base( list)
        {
            TotalCount = ( count > 0)? count:0;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CurrentPage = (currentPage > TotalPages)? TotalPages : currentPage;
        }

        public PaginatedList()
        {
            TotalCount = 0;
            TotalPages = 0;
            CurrentPage = 1;
        }
        public PaginatedList(PaginatedList<T> source) : base(source)
        {
            TotalCount = source.TotalCount;
            TotalPages = source.TotalPages;
            CurrentPage = source.CurrentPage;
        }
        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;
    }
    
}
