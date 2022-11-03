using BakeryRecipe.Application.System.Posts;
using BakeryRecipe.Application.System.Report;
using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Posts;
using BakeryRecipe.ViewModels.Report;
using BakeryRecipe.ViewModels.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BakeryRecipe.Api.Controllers
{
    [Route("api/reports")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateReport([FromBody] CreateReportRequest request)
        {
            var rs = await _reportService.CreateReport(request);
            return Ok(rs);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllReport([FromQuery] PaginationFilter filter)
        {
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize, filter._by, filter._order);
            var rs = await _reportService.GetAllReport(validFilter);
            return Ok(rs);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReport([FromRoute] int id)
        {
            var rs = await _reportService.GetReportDetail(id);
            return Ok(rs);
        }

        
    }
}
