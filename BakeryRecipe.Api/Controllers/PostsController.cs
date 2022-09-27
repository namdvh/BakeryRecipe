using BakeryRecipe.Application.System.Posts;
using BakeryRecipe.Application.System.Users;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Posts;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;

namespace BakeryRecipe.Api.Controllers
{
    [Route("api/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] AddPostRequest request)
        {
            BaseResponse<string> response = new();
            var rs = await _postService.CreatePost(request);
            if (rs)
            {
                response.Code = "200";
                response.Message = "Create succesfully";
            }
            else
            {
                response.Code = "202";
                response.Message = "Create unsuccesfully";
            }
            return Ok(response);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> CreatePost([FromRoute] int id)
        {
            BaseResponse<string> response = new();
            var rs = await _postService.DeletePost(id);
            if (rs)
            {
                response.Code = "200";
                response.Message = "Delete succesfully";
            }
            else
            {
                response.Code = "202";
                response.Message = "Delete unsuccesfully";
            }
            return Ok(response);
        }



        [HttpGet]
        public async Task<IActionResult> GetPost([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter._by, filter._order);
            var rs = await _postService.GetPost(validFilter);
            return Ok(rs);
        }

        [HttpGet("status")]
        public async Task<IActionResult> GetPostByStatusAndUserID([FromQuery] PaginationFilter filter ,int status,Guid userID )
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter._by, filter._order);
            var rs = await _postService.GetPostByStatusAndUserID(validFilter,status,userID);
            return Ok(rs);
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            var rs = await _postService.GetDetailPost(id);
            return Ok(rs);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPostByTitle([FromQuery] PaginationFilter filter, string keyword)
        {

            if (keyword != null)
            {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter._by, filter._order);
                var rs = await _postService.SearchPostByName(validFilter, keyword);

                return Ok(rs);
            }

            return BadRequest("Invalid method");
        }
       

        [HttpGet("category")]
        public async Task<IActionResult> GetPostByCategory([FromQuery] PaginationFilter filter,int categoryID)
        {
                var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter._by, filter._order);
                var rs = await _postService.SearchPostByCategories(validFilter, categoryID);

                return Ok(rs);

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost([FromRoute] int id, [FromBody] UpdatePostRequest request)
        {
            BaseResponse<string> response = new();
            var rs = await _postService.UpdatePost(request, id);
            if (rs)
            {
                response.Code = "200";
                response.Message = "Create succesfully";
            }
            else
            {
                response.Code = "202";
                response.Message = "Create unsuccesfully";
            }
            return Ok(response);
        }

    }
}
