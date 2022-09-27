using BakeryRecipe.Application.System.Users;
using BakeryRecipe.ViewModels.Comments;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;


namespace BakeryRecipe.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IConfiguration _configuration;
        public CommentController(ICommentService commentService, IConfiguration configuration)
        {
            _commentService = commentService;
            _configuration = configuration;
        }
        [HttpPost("cmt")]
        public async Task<IActionResult> AddComment([FromBody] CommentRequestDTO request)
        {
            BaseResponse<string> response = new();
            var result = await _commentService.AddComment(request);
            if (result!=null)
            {
                response.Code = "200";
                response.Message = "Comment succesfully";
            }
            else
            {
                response.Code = "202";
                response.Message = "Add comment unsuccesfully";
            }
            return Ok(response);
        }
    }
}
