using BakeryRecipe.Application.System.Users;
using BakeryRecipe.ViewModels.Interactive;
using Microsoft.AspNetCore.Mvc;

namespace BakeryRecipe.Api.Controllers
{
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
    }
}
