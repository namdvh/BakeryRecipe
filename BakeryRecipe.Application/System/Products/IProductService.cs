using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Products
{
    public interface IProductService
    {
        public Task<BasePagination<List<ProductDTO>>> GetAllProduct(PaginationFilter filter);
    }
}
