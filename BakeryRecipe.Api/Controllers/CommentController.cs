using BakeryRecipe.Application.Comments;
using BakeryRecipe.Application.System.Users;
using BakeryRecipe.ViewModels.Comments;
using BakeryRecipe.ViewModels.Response;
using BakeryRecipe.ViewModels.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace BakeryRecipe.Api.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IConfiguration _configuration;
        private IHubContext<SignalrHub, IMessageHubClient> messageHub;
        public CommentController(ICommentService commentService, IConfiguration configuration, IHubContext<SignalrHub, IMessageHubClient> messageHub)
        {
            _commentService = commentService;
            _configuration = configuration;
            this.messageHub = messageHub;
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
                await messageHub.Clients.All.SendOffersToUser(request);
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
