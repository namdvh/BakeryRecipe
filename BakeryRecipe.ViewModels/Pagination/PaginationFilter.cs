using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Pagination
{
    public class PaginationFilter
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string _by { get; set; }
        public int _order { get; set; }
        public bool _all { get; set; }



        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
            _by = string.Empty;
            _order = 1;
        }
        public PaginationFilter(int pageNumber, int pageSize, string sortBy, int orderBy)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
            _by = sortBy;
            _order = orderBy > 0 ? 1 : orderBy;
        }

        public PaginationFilter(int pageNumber, int pageSize, string sortBy, int orderBy, bool all)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize > 10 ? 10 : pageSize;
            _by = sortBy;
            _order = orderBy > 0 ? 1 : orderBy;
            _all = all;
        }


    }
}
