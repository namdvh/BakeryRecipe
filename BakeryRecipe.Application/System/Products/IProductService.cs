using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Products;
using BakeryRecipe.ViewModels.Response;
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
        public Task<BaseResponse<ProductDTO>> GetProducts(int productID);
        public Task<BaseResponse<string>> CreateProduct(CreateProductRequest request);  
        public Task<BaseResponse<string>> EditProduct(CreateProductRequest request,int productID);  
        public Task<BaseResponse<string>> DeleteProduct(int productID);  


    }
}
