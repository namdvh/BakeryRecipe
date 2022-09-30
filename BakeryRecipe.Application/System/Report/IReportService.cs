using BakeryRecipe.ViewModels.Pagination;
using BakeryRecipe.ViewModels.Products;
using BakeryRecipe.ViewModels.Report;
using BakeryRecipe.ViewModels.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BakeryRecipe.Application.System.Report
{
    public interface IReportService
    {
        public Task<BasePagination<List<ReportDTO>>> GetAllReport(PaginationFilter filter);
        public Task<BaseResponse<List<ReportDetailDTO>>> GetReportDetail(int postID);
        public Task<BaseResponse<string>> CreateReport(CreateReportRequest request);
    }
}
