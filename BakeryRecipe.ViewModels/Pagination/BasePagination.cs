using BakeryRecipe.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.ViewModels.Pagination
{
    public class BasePagination<T> : BaseResponse<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public BasePagination()
        {

        }

        public BasePagination(T data, int currentPage, int pageSize)
        {
            this.CurrentPage = currentPage;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Code = null;
        }
    }
}
