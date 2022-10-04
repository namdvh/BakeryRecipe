using BakeryRecipe.Application.System.Categories;
using BakeryRecipe.Application.System.Products;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryRecipe.Api.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllProducts([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter._by, filter._order,
                filter._all);
            var result = await _productService.GetAllProduct(validFilter);

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] int id)
        {
            var result = await _productService.GetProducts(id);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] CreateProductRequest request)
        {
            var result = await _productService.CreateProduct(request);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditProduct([FromBody] CreateProductRequest request, [FromRoute] int id)
        {
            var result = await _productService.EditProduct(request, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            var result = await _productService.DeleteProduct(id);

            return Ok(result);
        }
    }
}
