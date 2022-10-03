using BakeryRecipe.Application.System.Repost;
using BakeryRecipe.ViewModels.Repost;
using BakeryRecipe.ViewModels.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BakeryRecipe.Api.Controllers
{
    [Route("api/repost")]
    [ApiController]
    public class RepostController : ControllerBase
    {
        private readonly IRepostService _repostService;
        public RepostController(IRepostService repostService)
        {
            _repostService = repostService;
        }

        // GET api/<RepostController>/5
        [HttpGet("{UserId}")]
        public async Task<IActionResult> GetRepostByUsID(Guid UserId)
        {
            var rs = await _repostService.GetRepostByUsID(UserId);
            return Ok(rs);
        }

        // POST api/<RepostController>
        [HttpPost]
        public async Task<IActionResult> CreateRepost([FromBody] CreateRepostRequest request)
        {
            var rs = await _repostService.CreateRepost(request);
            return Ok(rs);
        }

        // DELETE api/<RepostController>/5
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteRepostRequest request)
        {
            BaseResponse<string> response = new();
            var rs = await _repostService.DeleteRepost(request);
            if (rs)
            {
                response.Code = "200";
                response.Message = "Delete repost succesfully";
            }
            else
            {
                response.Code = "202";
                response.Message = "Delete repost unsuccesfully";
            }
            return Ok(response);
        }
    }
}
