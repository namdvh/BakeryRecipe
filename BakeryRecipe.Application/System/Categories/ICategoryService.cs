using BakeryRecipe.Data.Entities;
using BakeryRecipe.ViewModels.Pagination;
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
    }
}
