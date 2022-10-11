using BakeryRecipe.Application.System.Categories;
using BakeryRecipe.Application.System.Users;
using BakeryRecipe.ViewModels.Categories;
using BakeryRecipe.ViewModels.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BakeryRecipe.Api.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter._by, filter._order,
                filter._all);
            var result = await _categoryService.GetAllCategories(validFilter);

            return Ok(result);

        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest request)
        {
            var result = await _categoryService.CreateCategory(request.CategoryName);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> EditCategory([FromBody] CategoryRequest request, [FromRoute] int id)
        {
            var result = await _categoryService.EditCategory(request.CategoryName, id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {

            var result = await _categoryService.DeleteCategory(id);

            return Ok(result);
        }
    }
}
