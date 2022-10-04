using BakeryRecipe.Application.System.Users;
using BakeryRecipe.ViewModels.Interactive;
using Microsoft.AspNetCore.Mvc;

namespace BakeryRecipe.Api.Controllers
{
    [Route("api/interactive")]
    [ApiController]
    public class InteractiveController : ControllerBase
    {
        private readonly IInteractiveService _interactiveService;
        private readonly IConfiguration _configuration;
        public InteractiveController(IInteractiveService interactiveService, IConfiguration configuration)
        {
            _interactiveService = interactiveService;
            _configuration = configuration;
        }
        [HttpPost("like")]
        public async Task<IActionResult> LikeAction([FromBody] LikeActionRequest request)
        {
            var result = await _interactiveService.LikeOrDislike(request.UserID, request.PostID, request.IsLike);

            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetInteractiveStatus(Guid userId,int postID)
        {
            var result = await _interactiveService.GetInteractiveStatus(userId, postID);

            return Ok(result);
        }

    }
}
