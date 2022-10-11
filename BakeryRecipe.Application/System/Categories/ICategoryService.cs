using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Categories
{
    public interface ICategoryService
    {
        public Task<BasePagination<List<Category>>> GetAllCategories(PaginationFilter filter);
        public Task<BaseResponse<string>> CreateCategory(string categoryName);
        public Task<BaseResponse<string>> EditCategory(string CategoryName, int categoryID);
        public Task<BaseResponse<string>> DeleteCategory(int categoryID);



    }
}
